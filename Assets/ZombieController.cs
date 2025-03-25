
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

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    private bool hangOn = false;

    private CameraHandler cameraHandler;

    private void Awake()
    {
        if (vanTransform == null && GameObject.FindWithTag("Player").TryGetComponent<Transform>(out Transform _vanTransform))
        {
            this.vanTransform = _vanTransform;
        }
        cameraHandler = Camera.main.transform.GetComponent<CameraHandler>();

    }

    private void OnEnable()
    {
        capsuleCollider.enabled = false;

        sign = Random.Range(-3, 3);
        if (sign <= 0)
        {
            sign = -1;
        }
        else
        {
            sign = 1;
        }

        offSetZ = Random.Range(0.4f, -0.45f);
        transform.position = new Vector3(vanTransform.position.x + sign * offSetX, 0, vanTransform.position.z + offSetZ);
        if (vanTransform.TryGetComponent<VanEngine>(out VanEngine vanEngine))
        {
            vanEngine.HangOnZombieNums++;
        }
    }

    private void OnDisable()
    {
        animation.Stop();
        animation.Rewind();
        animation.Play("Jump To Freehang");
        hangOn = false;
        if (vanTransform.TryGetComponent<VanEngine>(out VanEngine vanEngine))
        {
            vanEngine.HangOnZombieNums--;
        }

        GameManager.Instance.playerKill++;
        cameraHandler.Shake();
    }

    // Update is called once per frame
    void Update()
    {
        if (vanTransform != null)
        {

            if (Vector3.Distance(vanTransform.position, transform.position) > 1.05f)
            {
                transform.position = new Vector3(transform.position.x, vanTransform.position.y, vanTransform.position.z + offSetZ);
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
                if (transform.localPosition.x > -0.43f && transform.localPosition.x < 0.43f)
                {
                    if (transform.localPosition.x < 0)
                    {
                        transform.localPosition = new Vector3(-0.5f, transform.localPosition.y, transform.localPosition.z);
                    }
                    else
                    {
                        transform.localPosition = new Vector3(0.5f, transform.localPosition.y, transform.localPosition.z);
                    }
                }
                capsuleCollider.enabled = true;
            }

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, 0).normalized);
            transform.rotation = targetRotation;
        }
        else
        {
            this.enabled = false;
        }
    }
}
