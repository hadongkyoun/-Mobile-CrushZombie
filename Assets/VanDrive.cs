
using UnityEngine;

public class VanDrive : MonoBehaviour, IInputHandler
{

    [SerializeField]
    private VanData vanData;

    private Rigidbody rigidbody;
    [SerializeField]
    private VanEngine vanEngine;

    private float vanMinSideSpeed;
    private float vanMaxSideSpeed;
    private float vanStraightMaxSpeed;
    private float vanStraightMinSpeed;

    private float moveSmoothValue;


    private Vector3 moveVector;
    private Vector3 refVelocity;


    private float dir = 0.0f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();


        vanMinSideSpeed = vanData.SideMinimumSpeed;
        vanMaxSideSpeed = vanData.SideMaximumSpeed;

        vanStraightMaxSpeed = vanData.StraightMaximumSpeed;
        vanStraightMinSpeed = vanData.StraightMinimumSpeed;

        moveSmoothValue = vanData.MoveSmoothValue;
    }

    private void Update()
    {
        dir = SetDirection();
    }

    private void FixedUpdate()
    {
        Drive();
    }

    public void Drive()
    {
        if (GameManager.Instance.playerDead)
        {
            if(TryGetComponent<BoxCollider>(out BoxCollider collider))
            {
                collider.enabled = false;
            }
            return;
        }


        if (vanEngine == null)
        {
            Debug.Log("������ �Ƿ�");


        }
        if (vanData == null)
        {
            Debug.Log("dat�� ���÷�");
        }

        float sideSpeed;
        if (vanEngine.VanStraightSpeed <= vanStraightMinSpeed)
        {
            sideSpeed = vanMinSideSpeed;
        }
        else if (vanEngine.VanStraightSpeed < vanStraightMaxSpeed)
        {
            sideSpeed = vanMinSideSpeed + (vanEngine.VanStraightSpeed - vanStraightMinSpeed) / (vanStraightMaxSpeed - vanStraightMinSpeed)
                * (vanMaxSideSpeed - vanMinSideSpeed);
        }

        else
        {
            sideSpeed = vanMaxSideSpeed;
        }


        moveVector = new Vector3(dir * vanMinSideSpeed, 0, vanEngine.VanStraightSpeed / 4);



        // SmoothDamp�� ���Ͽ� ����� �н�
        rigidbody.linearVelocity = Vector3.SmoothDamp(rigidbody.linearVelocity, moveVector, ref refVelocity, moveSmoothValue);






        Quaternion targetRotation = Quaternion.LookRotation(rigidbody.linearVelocity);
        rigidbody.rotation = targetRotation;

        LimitVanPosition();
    }

    private void LimitVanPosition()
    {
        float currentX = transform.position.x;
        currentX = Mathf.Clamp(currentX, -2.5f, 2.5f);
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    public float SetDirection()
    {
        if (Application.isEditor)
        {
            dir = Input.GetAxisRaw("Horizontal");

        }

        else
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    dir = -1.0f;
                }
                else
                {
                    dir = 1.0f;
                }
            }
            else
            {
                dir = 0.0f;
            }
        }

        return dir;
    }
}
