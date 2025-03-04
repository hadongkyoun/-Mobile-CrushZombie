using UnityEngine;

public class TurnTableHandler : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;
    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
