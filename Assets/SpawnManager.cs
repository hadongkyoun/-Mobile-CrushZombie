

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

    [Tooltip("���� �����ϴ� �ð� ")]

    [SerializeField]
    private float obstacleSpawnTime = 3.0f;
    [Tooltip("��ֹ� ���ϱ� ���� ���� �������� �޷����� �ð�")]
    [SerializeField]
    private float runningTimeToHorde = 5.0f;

    private float currentTime = 0.0f;

    //[SerializeField] �ִϸ��̼� Ŭ���� �߰� �Ǵ� ��� ����
    private string[] zombieClips = { "Zombie1", "Zombie2" };

    [SerializeField]
    // �ش� �Ÿ���ŭ Van�� �̵��ϸ� Spawn
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
        // Van�� �ı������� ��ũ��Ʈ ��Ȱ��ȭ
        if (vanController == null)
        {
            Debug.Log("[ SpawnManager ] : Van is Destoryed");
            this.enabled = false;
            return;
        }

        currentTime += Time.deltaTime;


        // ��ֹ� ���� �ð�
        if (obstacleSpawnTime > currentTime)
        {
            // ����� van ��ġ�κ��� ���� �Ÿ� �����
            if (vanTransform != null && vanTransform.position.z >= spawnTriggerDistance + saveVanPosition.z)
            {
                saveVanPosition = vanTransform.position;
                Spawn();
            }
        }
        else
        {
            // ������ ���� ��ġ�� ����������
            if (vanTransform != null && !finishSpawning && vanTransform.position.z > saveVanPosition.z + spawnOffSetZ)
            {
                //SpawnHordeJombies();
                // van���� ��ֹ� ���ϱⰡ �������� �˸���.
                finishSpawn.Invoke();
                finishSpawning = true;
            }
        }
    }
    public void Spawn()
    {
        //if (!spawnHordeJombies)
        //{
            // ���� �� ������Ʈ ID
            id = ChooseSpawnID(Random.Range(0, 100));
        //}
        //// ��Ʈ ����Ʈ
        //else
        //{
        //    // ���� ����
        //    id = 0;
        //}
        // ������Ʈ Ȱ��ȭ
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
    //    // Spawn X �� ����
    //    spawnOffSetX = 0.8f;

    //    yield return new WaitForSecondsRealtime(runningTimeToHorde);


    //    // Van�� �ı����� �ʴ� ��
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



