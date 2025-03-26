
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

        // 차의 속도와 부스터에 영향 
        vanEngine.AffectEngineVelocity(obstacleData.DamageVelocity);
        // 차의 내구도와 콤보에 영향
        vanEngine.AffectEngineHP(obstacleData.DamageHp);

        if (obstacleData.Id == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
            // 차의 내구도와 콤보에 영향
            vanEngine.AffectEngineHP(obstacleData.DamageHp);
            return;
        }

        
        if (obstacleData.Id == 3)
        {
            if (vanEngine.HangOnZombieNums > 1)
            {
                // 좀비 소리 추가
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

        //// 현재 정보 업데이트
        //currentHP = crushManager.GetVanHP() / data.maxVanHP;
        //boosterAmount = vanEngine.GetBoosterAmount();

        //// UI 업데이트
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
