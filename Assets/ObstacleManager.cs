

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
        // 장애물로부터 플레이어가 일정거리 멀어지면
        if (vanTransform != null && vanTransform.position.z - transform.position.z > disappearOffSetZ)
        {
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // 장애물이 플레이어와 충돌
        if (other.gameObject.CompareTag("Player"))
        {

            if (other.TryGetComponent<VanController>(out VanController vanController))
            {

                // 장애물에 맞는 이펙트 활성화
                poolingManager.ActivateEffect(obstacleData.Id + 4, transform);

                // 플레이어에게 영향
                vanController.ObjectCollision(obstacleData);

            }


            // 충돌한 오브젝트는 비활성화
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }
    }


}
