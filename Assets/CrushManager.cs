
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

        // 차의 속도와 부스터에 영향 
        vanEngine.AffectEngineVelocity(obstacleData.DamageVelocity);
        // 차의 내구도와 콤보에 영향
        vanEngine.AffectEngineHP(obstacleData.DamageHp);

        if(obstacleData.Id == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
            return;
        }

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
