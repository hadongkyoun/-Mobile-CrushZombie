

using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private ObjectPoolingManager poolingManager;
    public void Initialization(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }


    public Obstacle obstacleData;


    private Transform vanTransform;
    private float disappearOffSetZ = 4;


    private void Awake()
    {
        if (vanTransform == null && GameObject.FindWithTag("Player").TryGetComponent<Transform>(out Transform _vanTransform))
        {
            this.vanTransform = _vanTransform;
        }
    }

    private void Update()
    {
        // ��ֹ��κ��� �÷��̾ �����Ÿ� �־�����
        if (vanTransform != null && vanTransform.position.z - transform.position.z > disappearOffSetZ)
        {
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // ��ֹ��� �÷��̾�� �浹
        if (other.gameObject.CompareTag("Player"))
        {

            if (other.TryGetComponent<VanController>(out VanController vanController))
            {

                // ��ֹ��� �´� ����Ʈ Ȱ��ȭ
                poolingManager.ActivateEffect(obstacleData.Id + 4, transform);

                // �÷��̾�� ����
                vanController.ObjectCollision(obstacleData);

            }


            // �浹�� ������Ʈ�� ��Ȱ��ȭ
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }
    }


}
