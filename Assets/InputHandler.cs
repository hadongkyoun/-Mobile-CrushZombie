using UnityEngine;

public class InputHandler : IInputHandler
{
    private float dir = 0.0f;
    public float Direction { get { return dir; } }
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
