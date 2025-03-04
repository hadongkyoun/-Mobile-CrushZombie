
using UnityEngine;

public class VanDrive
{

    private Rigidbody rigidbody;
    private Transform transform;

    private float vanMinSideSpeed;
    private float vanMaxSideSpeed;
    private float vanStraightMaxSpeed;
    private float vanStraightMinSpeed;

    private float moveSmoothValue;


    private Vector3 moveVector;
    private Vector3 refVelocity;

    private bool playerDriving = true;

    // 생성자로 초기화
    public VanDrive(VanData _data, Rigidbody _rigidbody, Transform _transform)
    {
        rigidbody = _rigidbody;
        transform = _transform;
        
        vanMinSideSpeed = _data.SideMinimumSpeed;
        vanMaxSideSpeed = _data.SideMaximumSpeed;

        vanStraightMaxSpeed = _data.StraightMaximumSpeed;
        vanStraightMinSpeed = _data.StraightMinimumSpeed;

        moveSmoothValue = _data.MoveSmoothValue;
    }

    public void Drive(float vanStraightSpeed, float x)
    {


        // 드라이빙 열림
        if (playerDriving)
        {
            float sideSpeed;
            if(vanStraightSpeed <= 40.0f)
            {
                sideSpeed = vanMinSideSpeed;
            }
            else if(vanStraightSpeed < 130.0f)
            {
                sideSpeed = vanMinSideSpeed + (vanStraightSpeed - vanStraightMinSpeed) / (vanStraightMaxSpeed - vanStraightMinSpeed)
                    * (vanMaxSideSpeed - vanMinSideSpeed);
            }

            else
            {
                sideSpeed = vanMaxSideSpeed;
            }

            
            moveVector = new Vector3(x * vanMinSideSpeed, 0, vanStraightSpeed / 4);

            

            LimitVanPosition();

            // SmoothDamp에 대하여 제대로 학습
            rigidbody.linearVelocity = Vector3.SmoothDamp(rigidbody.linearVelocity, moveVector, ref refVelocity, moveSmoothValue);
            //}

        }
        // 드라이빙 잠김
        else
        {
            rigidbody.linearVelocity = new Vector3(-transform.position.x, 0, vanStraightSpeed / 4);
        }

        Quaternion targetRotation = Quaternion.LookRotation(rigidbody.linearVelocity);
        rigidbody.rotation = targetRotation;
    }

    private void LimitVanPosition()
    {
        float currentX = transform.position.x;
        currentX = Mathf.Clamp(currentX, -2.5f, 2.5f);
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    public void LimitPlayerDriving()
    {
        playerDriving = false;
    }

}
