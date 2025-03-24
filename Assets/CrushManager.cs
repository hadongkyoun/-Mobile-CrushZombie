
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

    private void Update()
    {
        if (startCount)
        {
            waitNextCurrentKillTime += Time.deltaTime;
        }

        if(waitNextCurrentKillTime > waitNextKillTime)
        {
            startCount = false;
            waitNextCurrentKillTime = 0.0f;
        }
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

        if(obstacleData.Id == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);

            return;
        }

        if (obstacleData.Id == 0)
        {
            if (startCount == true && !multiKillReset)
            {
                multiKillReset = true;
                // �޺� UI&SOUND ����
                AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_MULTIPLEKILL);
                UIManager.Instance.ActivateSpeedUpAnim();
                vanEngine.AffectEngineVelocity(-5.0f);
                StartCoroutine(turnStartFalse());
            }
            else
            {
                startCount = true;
            }
        }
        if(obstacleData.Id == 3)
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
