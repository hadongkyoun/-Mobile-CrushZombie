

using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private VanController vanController;
    private Transform vanTransform;

    private ObjectPoolingManager poolingManager;
    private UnityAction finishSpawn;


    [SerializeField]
    private float spawnOffSetZ = 35f;

    [SerializeField]
    private float spawnOffSetX = 2.85f;

    [Tooltip("게임 스폰하는 시간 ")]

    [SerializeField]
    private float obstacleSpawnTime = 3.0f;
    [Tooltip("장애물 피하기 이후 좀비 무리까지 달려가는 시간")]
    [SerializeField]
    private float runningTimeToHorde = 5.0f;

    private float currentTime = 0.0f;

    //[SerializeField] 애니메이션 클립이 추가 되는 경우 수정
    private string[] zombieClips = { "Zombie1", "Zombie2" };

    [SerializeField]
    // 해당 거리만큼 Van이 이동하면 Spawn
    private float spawnTriggerDistance = 5.0f;
    private Vector3 saveVanPosition;

    private bool spawnHordeJombies;

    private bool finishSpawning = false;
    private int id;

    private void Awake()
    {
        poolingManager = GetComponent<ObjectPoolingManager>();
    }
    private void Start()
    {
        finishSpawn += vanController.FinishAvoiding;
        vanTransform = vanController.transform;
        saveVanPosition = vanTransform.position;
    }

    private void Update()
    {
        // Van이 파괴됐을때 스크립트 비활성화
        if (vanController == null)
        {
            Debug.Log("[ SpawnManager ] : Van is Destoryed");
            this.enabled = false;
            return;
        }

        currentTime += Time.deltaTime;


        // 장애물 생성 시간
        if (obstacleSpawnTime > currentTime)
        {
            // 저장된 van 위치로부터 일정 거리 벗어나면
            if (vanTransform != null && vanTransform.position.z >= spawnTriggerDistance + saveVanPosition.z)
            {
                saveVanPosition = vanTransform.position;
                Spawn();
            }
        }
        else
        {
            // 마지막 스폰 위치를 지나갔을때
            if (vanTransform != null && !finishSpawning && vanTransform.position.z > saveVanPosition.z + spawnOffSetZ)
            {
                //SpawnHordeJombies();
                // van에게 장애물 피하기가 끝났음을 알린다.
                finishSpawn.Invoke();
                finishSpawning = true;
            }
        }
    }
    public void Spawn()
    {
        //if (!spawnHordeJombies)
        //{
            // 스폰 할 오브젝트 ID
            id = ChooseSpawnID(Random.Range(0, 100));
        //}
        //// 라스트 스퍼트
        //else
        //{
        //    // 좀비만 생성
        //    id = 0;
        //}
        // 오브젝트 활성화
        if (vanTransform.TryGetComponent<VanController>(out VanController vanController))
        {
            if (vanController.HangNumFull == false)
            {

                poolingManager.ActivateObstacle
                                (id, SetObstaclePosition(), SetObstacleRotation());
                if (id == 0 && !spawnHordeJombies)
                {
                    poolingManager.ActivateObstacle
                                        (2, SetObstaclePosition(), SetObstacleRotation());
                }
            }

            else
            {
                poolingManager.ActivateObstacle
                                    (2, SetObstaclePosition(), SetObstacleRotation());
            }
        }
    }

    private int ChooseSpawnID(int randomIndex)
    {
        if (randomIndex >= 0 && randomIndex <= 30)
        {
            return 0;
        }
        else if (randomIndex <= 70)
        {
            return 0;
        }
        else if (randomIndex <= 90)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    private Vector3 SetObstaclePosition()
    {

        return new Vector3(Random.Range(-spawnOffSetX, spawnOffSetX), 0,
                    vanTransform.position.z + spawnOffSetZ);
    }

    private Quaternion SetObstacleRotation()
    {
        

        float randomY = Random.Range(0f, 360f);
        return Quaternion.Euler(0, randomY, 0);

    }

    //public void SpawnHordeJombies()
    //{
    //    if (!spawnHordeJombies)
    //    {
    //        spawnHordeJombies = true;
    //        StartCoroutine(SpawnHorde());
    //    }

    //}

    //IEnumerator SpawnHorde()
    //{
    //    spawnTriggerDistance = 2.7f;
    //    // Spawn X 값 수정
    //    spawnOffSetX = 0.8f;

    //    yield return new WaitForSecondsRealtime(runningTimeToHorde);


    //    // Van이 파괴되지 않는 한
    //    while (vanTransform != null)
    //    {

    //        if (vanTransform.position.z - saveVanPosition.z >= spawnTriggerDistance)
    //        {
    //            int spawnNum = Random.Range(3, 6);

    //            for (int i = 0; i < spawnNum; i++)
    //            {
    //                Spawn();
    //            }
    //            saveVanPosition = vanTransform.position;
    //        }
    //        yield return null;
    //    }
    //}
}



