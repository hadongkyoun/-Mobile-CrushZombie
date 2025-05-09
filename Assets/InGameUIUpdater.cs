
using UnityEngine;
using UnityEngine.UI;

public class InGameUIUpdater : MonoBehaviour
{

    private VanEngine vanEngine;
    private ResultUpdater resultUpdater;

    [Space(3.0f)]
    [Header("Van UI")]
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private Text speedText;

    private bool sendResult = false;



    [SerializeField]
    private Animation speedUpAnimation;

    [SerializeField]
    private Text time;

    [SerializeField]
    private Text comboText;



    private void Awake()
    {
        Debug.Log("Awake 시작");
        resultUpdater = GetComponentInChildren<ResultUpdater>();
    }

    private void Update()
    {
        if (!GameManager.Instance.playerDead)
        {
            time.text = $"{GameManager.Instance.GetPlayTime}:F0";

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

    public void ResetComboText()
    {
        comboText.text = "";
    }
    public void UpdateComboText(int combo)
    {
        comboText.text = $"Combo {combo}";
        if (comboText.TryGetComponent<Animation>(out Animation animation))
        {
            animation.Play();
        }
    }

    public void ActivateSpeedUpAnim()
    {
        speedUpAnimation.Play();
    }

}
