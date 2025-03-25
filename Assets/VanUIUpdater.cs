
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
        Debug.Log("Awake ����");
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
            // ü�� UI ������Ʈ
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
    //    // �޺������� ���ǵ尡 �����ϴ� ���
    //    speedUp.Play();
    //    AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
    //}

}
