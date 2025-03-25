
using UnityEngine;
using UnityEngine.UI;

public class VanUIUpdater : MonoBehaviour
{

    private VanEngine vanEngine;
    private ResultUpdater resultUpdater;

    [Space(3.0f)]
    [Header("Van UI")]
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Animation speedUp;

    private bool sendResult = false;

    private void Awake()
    {
        Debug.Log("Awake 시작");
        resultUpdater = GetComponentInChildren<ResultUpdater>();
    }

    private void Update()
    {
        if (!GameManager.Instance.playerDead)
        {
            

            if (vanEngine == null)
            {

                if (GameObject.FindWithTag("Player").TryGetComponent<VanEngine>(out VanEngine _vanEngine))
                    vanEngine = _vanEngine;
                sendResult = false;
            }
            // 체력 UI 업데이트
            hpImage.fillAmount = vanEngine.CurrentHP / vanEngine.MaxHP;

            if (vanEngine.UpdateSpeedTrigger)
            {
                vanEngine.UpdateSpeedTrigger = false;
                SpeedUpdate(vanEngine.VanStraightSpeed/2);
            }
        }

        else
        {
            if (!sendResult)
            {
                resultUpdater.UpdateResultValue();
                sendResult = true;
                Debug.Log("Send");
            }
        }
    }


    public void SpeedUpdate(float vanStraightSpeed)
    {
        speedText.text = $"{vanStraightSpeed:F0}KM";
    }

    //public void ComboSpeedUpEffect()
    //{
    //    // 콤보로인해 스피드가 급증하는 경우
    //    speedUp.Play();
    //    AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
    //}

}
