
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
        if (vanEngine == null)
        {
            Debug.Log("엔진ㅇ ㅗ류");


        }
        if (vanData == null)
        {
            Debug.Log("datㅁ 오ㅓ류");
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



        // SmoothDamp에 대하여 제대로 학습
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

    private float targetDir = 0.0f;

    public float SetDirection()
    {
        if (Application.isEditor)
        {
            dir = Input.GetAxis("Horizontal");
        }

        else
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    targetDir = -1.0f;
                }
                else
                {
                    targetDir = 1.0f;
                }
            }
            else
            {
                targetDir = 0.0f;
            }
        }

        return Mathf.MoveTowards(dir, targetDir, Time.deltaTime * 30.0f);
    }
}
