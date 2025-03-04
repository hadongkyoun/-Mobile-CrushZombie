using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VanEngine
{

    private float vanStraightSpeed;
    private float vanBeforeSpeed;
    private float vanMaxSpeed;
    private float vanMinSpeed;

    private float increaseValue;
    private float firstIncreaseValue;
    private float boosterSpeedValue;


    private bool activateBooster;
    private bool lastSpurtActivate;

    private float engineBoosterAmount;
    private float beforeBoosterAmount;

    private event Action<float, float> uiSpeedUpdate;
    private event Action<float> uiBoosterUpdate;

    public VanEngine(VanData vanData, VanUIUpdater _vanUIUpdater)
    {
        vanStraightSpeed = vanData.StraightSpeed;
        vanMaxSpeed = vanData.StraightMaximumSpeed;
        vanMinSpeed = vanData.StraightMinimumSpeed;
        vanBeforeSpeed = vanStraightSpeed;
        increaseValue = vanData.IncreaseValue;
        firstIncreaseValue = increaseValue;
        boosterSpeedValue = vanData.boosterSpeedValue;

        uiSpeedUpdate += _vanUIUpdater.SpeedUpdate;
        uiBoosterUpdate += _vanUIUpdater.BoosterUpdate;

    }

    public void RunningEngine()
    {
        Booster();

        vanStraightSpeed += Time.deltaTime * increaseValue;

        // ���� �ӵ��� ������ �������� ���
        if (vanStraightSpeed <= vanMinSpeed)
        {
            vanStraightSpeed = 0f;
        }

        if(!lastSpurtActivate && vanStraightSpeed > vanMaxSpeed)
        {
            vanStraightSpeed = vanMaxSpeed;
        }


            // UI ������Ʈ
        if (Mathf.Abs(vanBeforeSpeed - vanStraightSpeed) >= 1.0f)
        {
            uiSpeedUpdate?.Invoke(vanBeforeSpeed, vanStraightSpeed);
            vanBeforeSpeed = vanStraightSpeed;
        }

    }

    public void AffectEngine(Obstacle obstacleData)
    {
        float damage = obstacleData.DamageVelocity;
        if (damage <= 0.1f)
        {
            damage = 0.1f;
        }

        if (obstacleData.Id == 0)
        {
            // ��Ʈ ����Ʈ ��Ȳ�� �ƴ� ��� ����� �ε����� �ν��� ����
            if (!lastSpurtActivate)
            {
                engineBoosterAmount += 0.025f;
            }
            else
            {
                
                vanStraightSpeed -= damage;
                
            }
        }
        else
        {
            engineBoosterAmount -= 0.1f;

            vanStraightSpeed -= damage;
        }

    }

    public void ComboSpeedUP()
    {
        vanStraightSpeed += 3.5f;
    }

    public float GetVanSpeed()
    {
        return vanStraightSpeed;
    }



    private void Booster()
    {
        
        if (activateBooster)
        {
            increaseValue = 10.0f + boosterSpeedValue;
            engineBoosterAmount -= Time.deltaTime / 4.5f;
            if (engineBoosterAmount <= 0.05f)
            {
                activateBooster = false;
                engineBoosterAmount = 0;
            }
            uiBoosterUpdate.Invoke(engineBoosterAmount);
        }
        else
        {
            increaseValue = firstIncreaseValue;
        }

        if (beforeBoosterAmount != engineBoosterAmount)
        {   

            beforeBoosterAmount = engineBoosterAmount;
        }

    }
    public float GetBoosterAmount()
    {
        return engineBoosterAmount;
    }


    public void LastSpurt()
    {
        activateBooster = true;
        increaseValue = 10.0f;

        lastSpurtActivate = true;
    }

}
