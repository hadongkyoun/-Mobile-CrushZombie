using System;
public class CrushManager
{
    private float engineHP;
    private float currentHP;

    private float kill = 0;
    private int combo = 0;
    private bool lastSpurtActivate;

    private event Action comboSpeedUp;

    public CrushManager(VanData vanData, VanUIUpdater _vanUIUpdater)
    {

        engineHP = vanData.maxVanHP;
        currentHP = engineHP;

        comboSpeedUp += _vanUIUpdater.ComboSpeedUpEffect;
    }

    public void AffectVan(Obstacle obstacleData)
    {
        if (obstacleData.Id == 0)
        {
            // 라스트 스퍼트 상황일때
            if (lastSpurtActivate)
            {
                currentHP -= obstacleData.DamageHp;
            }
        }
        else
        {
            currentHP -= obstacleData.DamageHp;
        }
    }

    public void LastSpurt()
    {
        lastSpurtActivate = true;
    }

    public float GetVanHP()
    {
        return currentHP;
    }

    public int UpdateCombo(Obstacle obstacleData)
    {
        if (obstacleData.Id != 0)
        {
            combo = 0;
        }
        else
        {
            if (!lastSpurtActivate)
            {
                combo++;
            }
            kill+=1;
        }

        return combo;
    }

    public float ZombieKillNum()
    {
        return kill;
    }

    public bool IsMultiplesOfFive()
    {
        if (lastSpurtActivate)
        {
            return false;
        }

        if (combo > 0 && combo % 5 == 0)
        {
            //Bust
            comboSpeedUp.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }


}
