

using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawnManager : MonoBehaviour
{

    [SerializeField]
    private Transform vanTransform;

    private ObjectPoolingManager poolingManager;
    private UnityAction finishSpawn;


    [SerializeField]
    private float spawnOffSetZ = 35f;

    [SerializeField]
    private float spawnOffSetX = 2.85f;

    private float currentTime = 0.0f;

    private float obstacleSpawnTime;

    [SerializeField]
    private CinemachineBrain cinemachineBrain;

    //[SerializeField] 애니메이션 클립이 추가 되는 경우 수정
    private string[] zombieClips = { "Zombie1", "Zombie2" };

    [SerializeField]
    // 해당 거리만큼 Van이 이동하면 Spawn
    private float spawnTriggerDistance = 5.0f;
    private Vector3 saveVanPosition;


    private bool finishSpawning = false;
    private int id;

    private void Awake()
    {
        poolingManager = GetComponent<ObjectPoolingManager>();
    }
    private void Start()
    {
        vanTransform = vanTransform.transform;
        saveVanPosition = vanTransform.position;
    }

    private void Update()
    {
        // Van이 파괴됐을때 스크립트 비활성화
        if (vanTransform == null)
        {
            Debug.Log("[ SpawnManager ] : Van is Destoryed");
            this.enabled = false;
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime > GameManager.Instance.GetPlayTime)
        {
            cinemachineBrain.enabled = false;
            return;
        }

        // 저장된 van 위치로부터 일정 거리 벗어나면
        if (vanTransform != null && vanTransform.position.z >= spawnTriggerDistance + saveVanPosition.z)
        {
            saveVanPosition = vanTransform.position;
            Spawn();
        }

        //else
        //{
        //    // 마지막 스폰 위치를 지나갔을때
        //    if (vanTransform != null && !finishSpawning && vanTransform.position.z > saveVanPosition.z + spawnOffSetZ)
        //    {
        //        //SpawnHordeJombies();
        //        // van에게 장애물 피하기가 끝났음을 알린다.
        //        finishSpawn.Invoke();
        //        finishSpawning = true;
        //    }
        //}
    }
    public void Spawn()
    {
        // 스폰 할 오브젝트 ID
        id = ChooseSpawnID(Random.Range(0, 100));


        poolingManager.ActivateObstacle
                        (id, SetObstaclePosition(), SetObstacleRotation());


    }

    private int ChooseSpawnID(int randomIndex)
    {
        float[] cumulativeWeights = { 0.04f, 0.71f, 1.0f }; // 누적 확률
        float randomValue = Random.value; // 0.0 ~ 1.0 사이의 난수

        if (randomValue < cumulativeWeights[0])
            return 1; // 4% 확률
        else if (randomValue < cumulativeWeights[1])
            return 2; // 67% 확률
        else
            return 3; // 29% 확률
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
}



