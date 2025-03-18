using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Transform vanTransform;
    private int sign;
    private int offSetX = 3;
    private float offSetZ;
    private Vector3 dir;
    [SerializeField]
    private int speed;
    [SerializeField]
    private Animation animation;

    private bool hangOn = false;
    private void Awake()
    {
        if(vanTransform == null && TryGetComponent<ObstacleManager>(out ObstacleManager obstacleManager))
        {
            vanTransform = obstacleManager.VanTransform;
        }
        
    }

    private void OnEnable()
    {

        sign = Random.Range(-3, 3);
        if (sign <= 0)
        {
            sign = -1;
        }
        else
        {
            sign = 1;
        }

        offSetZ = Random.Range(0.3f, -0.4f);
        transform.position = new Vector3(vanTransform.position.x + sign * offSetX, 0, vanTransform.position.z + offSetZ);

        // 좀비 수 증가
        if (vanTransform.TryGetComponent<VanController>(out VanController vanController))
        {
            vanController.zombieHangIncrease();
        }
    }

    private void OnDisable()
    {
        animation.Stop();
        animation.Rewind();
        animation.Play("Jump To Freehang");
        hangOn = false;

        // 좀비 수 감소
        if (vanTransform.TryGetComponent<VanController>(out VanController vanController))
        {
            vanController.zombieHangDecrease();
        }
    }

    // Update is called once per frame
    void Update()
    {
;

        if (Vector3.Distance(vanTransform.position, transform.position) > 1.25f)
        {
            transform.position = new Vector3(transform.position.x, vanTransform.position.y, vanTransform.position.z+ offSetZ);
            dir = (vanTransform.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }
        else
        {
            hangOn = true;
        }

        if (hangOn)
        {
            animation.Play("Hanging Idle");
            transform.SetParent(vanTransform);
        }

        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, 0).normalized);
        transform.rotation = targetRotation;

    }

}
