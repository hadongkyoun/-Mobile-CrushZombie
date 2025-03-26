
using System.Collections;
using UnityEngine;
public class CrushManager : MonoBehaviour
{
    [SerializeField]
    private VanEngine vanEngine;
    private CameraHandler cameraHandler;

    [SerializeField]
    private InGameUIUpdater inGameUIUpdater;

    private float kill = 0;


    private int combo = 0;
    private bool lastSpurtActivate;

    private bool multiKillReset = false;

    private void Awake()
    {
        cameraHandler = Camera.main.transform.GetComponent<CameraHandler>();
    }

    IEnumerator turnStartFalse()
    {
        yield return new WaitForSeconds(0.3f);
        multiKillReset = false;
    }


    public void ObjectCollision(Obstacle obstacleData)
    {

        if (obstacleData.Id == 0)
        {
            
            if (!multiKillReset)
            {
                multiKillReset = true;
                combo++;
                inGameUIUpdater.UpdateComboText(combo);

                StartCoroutine(turnStartFalse());
            }
            else
            {
                GameManager.Instance.GetPlayTime += 10.0f;
                AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_KEEPGOING);
            }

            if (combo > 0 && combo % 5 == 0)
            {
                vanEngine.SpeedUp();
                AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_SPEEDUP);
                inGameUIUpdater.ActivateSpeedUpAnim();
            }


            return;
        }

        // ���� �ӵ��� �ν��Ϳ� ���� 
        vanEngine.AffectEngineVelocity(obstacleData.DamageVelocity);
        // ���� �������� �޺��� ����
        vanEngine.AffectEngineHP(obstacleData.DamageHp);

        if (obstacleData.Id == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
            // ���� �������� �޺��� ����
            vanEngine.AffectEngineHP(obstacleData.DamageHp);
            return;
        }

        
        if (obstacleData.Id == 3)
        {
            if (vanEngine.HangOnZombieNums > 1)
            {
                // ���� �Ҹ� �߰�
                AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_ZOMBIE);
            }
            combo = 0;
        }
        if(obstacleData.Id == 2)
        {
            combo = 0;
        }
        inGameUIUpdater.ResetComboText();
        cameraHandler.Shake();

        //// ���� ���� ������Ʈ
        //currentHP = crushManager.GetVanHP() / data.maxVanHP;
        //boosterAmount = vanEngine.GetBoosterAmount();

        //// UI ������Ʈ
        //NotifyObservers();
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
    
}
