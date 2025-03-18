using System.Runtime.CompilerServices;
using UnityEngine;


public class VanData
{

    

    private float straightSpeed = 60;
    public float StraightSpeed { get { return straightSpeed; } }
    private float straightMaximumSpeed = 160.0f;
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
    private float increaseValue = 5f;
    public float IncreaseValue { get { return increaseValue; } }
    

    public float maxVanHP = 20;
    public float boosterSpeedValue;


    public VanData()
    {
        DataManager.Instance.SetVanData(this);
    }
}
