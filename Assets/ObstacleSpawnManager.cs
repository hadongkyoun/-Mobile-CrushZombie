

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

    //[SerializeField] �ִϸ��̼� Ŭ���� �߰� �Ǵ� ��� ����
    private string[] zombieClips = { "Zombie1", "Zombie2" };

    [SerializeField]
    // �ش� �Ÿ���ŭ Van�� �̵��ϸ� Spawn
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
        // Van�� �ı������� ��ũ��Ʈ ��Ȱ��ȭ
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

        // ����� van ��ġ�κ��� ���� �Ÿ� �����
        if (vanTransform != null && vanTransform.position.z >= spawnTriggerDistance + saveVanPosition.z)
        {
            saveVanPosition = vanTransform.position;
            Spawn();
        }

        //else
        //{
        //    // ������ ���� ��ġ�� ����������
        //    if (vanTransform != null && !finishSpawning && vanTransform.position.z > saveVanPosition.z + spawnOffSetZ)
        //    {
        //        //SpawnHordeJombies();
        //        // van���� ��ֹ� ���ϱⰡ �������� �˸���.
        //        finishSpawn.Invoke();
        //        finishSpawning = true;
        //    }
        //}
    }
    public void Spawn()
    {
        // ���� �� ������Ʈ ID
        id = ChooseSpawnID(Random.Range(0, 100));


        poolingManager.ActivateObstacle
                        (id, SetObstaclePosition(), SetObstacleRotation());


    }

    private int ChooseSpawnID(int randomIndex)
    {
        float[] cumulativeWeights = { 0.04f, 0.71f, 1.0f }; // ���� Ȯ��
        float randomValue = Random.value; // 0.0 ~ 1.0 ������ ����

        if (randomValue < cumulativeWeights[0])
            return 1; // 4% Ȯ��
        else if (randomValue < cumulativeWeights[1])
            return 2; // 67% Ȯ��
        else
            return 3; // 29% Ȯ��
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



