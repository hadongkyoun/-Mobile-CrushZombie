
using UnityEngine;

public class VanData : MonoBehaviour
{

    private void Awake()
    {
        DataManager.Instance.SetVanData(this);
    }

    private float straightSpeed = 50;
    public float StraightSpeed { get { return straightSpeed; } }
    private float straightMaximumSpeed = 180.0f;
    public float StraightMaximumSpeed { get { return straightMaximumSpeed; } }
    private float straightMinimumSpeed = 20.0f;
    public float StraightMinimumSpeed { get { return straightMinimumSpeed; } }


    private float sideSpeed = 8;
    public float SideSpeed { get {  return sideSpeed; } }

    private float sideMinimumSpeed = 8.0f;
    public float SideMinimumSpeed { get { return sideMinimumSpeed; } }
    private float sideMaximumSpeed = 14.0f;
    public float SideMaximumSpeed { get { return sideMaximumSpeed; } }

    private float moveSmoothValue = 0.05f;
    public float MoveSmoothValue {  get { return moveSmoothValue; } }
    private float increaseValue = 3.5f;
    public float IncreaseValue { get { return increaseValue; } }
    

    private float maxVanHP = 20;
    public float MaxVanHP { get {  return maxVanHP; } set { maxVanHP = value; } }


   
}
