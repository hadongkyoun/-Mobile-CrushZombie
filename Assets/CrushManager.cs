
using UnityEngine;
public class CrushManager : MonoBehaviour
{
    [SerializeField]
    private VanEngine vanEngine;
    private CameraHandler cameraHandler;
 

    private float kill = 0;


    private int combo = 0;
    private bool lastSpurtActivate;

    private void Awake()
    {
        cameraHandler = Camera.main.transform.GetComponent<CameraHandler>();
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
