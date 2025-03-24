
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
    public Transform VanTransform { get { return vanTransform; } }

    private float disappearOffSetZ = 5;


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
        if (vanTransform != null)
        {
            if (vanTransform.position.z - transform.position.z > disappearOffSetZ && obstacleData.Id != 0)
                poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }
        else
        {
            this.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 장애물이 플레이어와 충돌
        if (obstacleData.Id != 0 && obstacleData.Id != 1 && other.gameObject.CompareTag("Player"))
        {

            if (other.TryGetComponent<CrushManager>(out CrushManager crushManager))
            {

                // 장애물에 맞는 이펙트 활성화
                poolingManager.ActivateEffect(obstacleData.Id + 4, transform);

                // 플레이어에게 영향
                crushManager.ObjectCollision(obstacleData);
                switch (obstacleData.Id)
                {
                    case 2:
                        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_ROCK);
                        break;
                    case 3:
                        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_BARREL);
                        break;
                    default:
                        break;
                }
                Debug.Log("Collision");
            }


            // 충돌한 오브젝트는 비활성화
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }

        else if (obstacleData.Id == 0 && (other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Barrel")))
        {
            // 장애물에 맞는 이펙트 활성화
            poolingManager.ActivateEffect(obstacleData.Id + 4, transform);
            if (other.gameObject.transform.TryGetComponent<ObstacleManager>(out ObstacleManager obstacleManager))
            {
                poolingManager.ActivateEffect(obstacleManager.obstacleData.Id + 4, other.gameObject.transform);
                switch (obstacleManager.obstacleData.Id)
                {
                    case 2:
                        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_ROCK);
                        poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
                        break;
                    case 3:
                        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_BARREL);
                        poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
                        break;
                    default:
                        break;
                }
            }
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_ZOMBIE);
            // 충돌한 오브젝트는 비활성화
            if (vanTransform != null && vanTransform.TryGetComponent<CrushManager>(out CrushManager crushManager))
            {
                crushManager.ObjectCollision(obstacleData);
            }

        }
        else if(obstacleData.Id == 0 && other.gameObject.CompareTag("Explosion"))
        {
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }

        else if (obstacleData.Id == 1 && other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<CrushManager>(out CrushManager crushManager) && other.TryGetComponent<VanEngine>(out VanEngine vanEngine))
            {
                if(vanEngine.CurrentHP >= vanEngine.MaxHP)
                {
                    return;
                }

                crushManager.ObjectCollision(obstacleData);
                poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);

            }

        }
    }


}
