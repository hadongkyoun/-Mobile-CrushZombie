
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
        // ��ֹ��κ��� �÷��̾ �����Ÿ� �־�����
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
        // ��ֹ��� �÷��̾�� �浹
        if (obstacleData.Id != 0 && obstacleData.Id != 1 && other.gameObject.CompareTag("Player"))
        {

            if (other.TryGetComponent<CrushManager>(out CrushManager crushManager))
            {

                // ��ֹ��� �´� ����Ʈ Ȱ��ȭ
                poolingManager.ActivateEffect(obstacleData.Id + 4, transform);

                // �÷��̾�� ����
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


            // �浹�� ������Ʈ�� ��Ȱ��ȭ
            poolingManager.DeActivateObject(obstacleData.Id, this.gameObject);
        }

        else if (obstacleData.Id == 0 && (other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Barrel")))
        {
            // ��ֹ��� �´� ����Ʈ Ȱ��ȭ
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
            // �浹�� ������Ʈ�� ��Ȱ��ȭ
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
