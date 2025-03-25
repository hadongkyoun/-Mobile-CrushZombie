
using System.Collections;
using UnityEngine;
public class CrushManager : MonoBehaviour
{
    [SerializeField]
    private VanEngine vanEngine;
    private CameraHandler cameraHandler;
 

    private float kill = 0;


    private int combo = 0;
    private bool lastSpurtActivate;

    [SerializeField]
    private float waitNextKillTime = 0.01f;
    private float waitNextCurrentKillTime = 0.0f;

    private bool startCount = false;
    private bool multiKillReset = false;

    private void Awake()
    {
        cameraHandler = Camera.main.transform.GetComponent<CameraHandler>();
    }

    IEnumerator turnStartFalse()
    {
        yield return new WaitForSeconds(0.2f);
        multiKillReset = false;
    }

    public void ObjectCollision(Obstacle obstacleData)
    {

        // ���� �ӵ��� �ν��Ϳ� ���� 
        vanEngine.AffectEngineVelocity(obstacleData.DamageVelocity);
        // ���� �������� �޺��� ����
        vanEngine.AffectEngineHP(obstacleData.DamageHp);

        if (obstacleData.Id == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);

            return;
        }

        if (obstacleData.Id == 0)
        {
            
            if (!multiKillReset)
            {
                multiKillReset = true;
                vanEngine.SpeedUp();
                if(Random.Range(0,10) %2 == 0)
                {
                AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_SPEEDUP);

                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_KEEPGOING);
                }
                UIManager.Instance.ActivateSpeedUpAnim();
                StartCoroutine(turnStartFalse());
            }
        }
        if (obstacleData.Id == 3)
        {
            if (vanEngine.HangOnZombieNums > 1)
            {
                // ���� �Ҹ� �߰�
                //AudioManager.Instance.PlaySFX()
            }
        }

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
