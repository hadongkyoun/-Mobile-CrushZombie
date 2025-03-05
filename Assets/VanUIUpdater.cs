
using UnityEngine;
using UnityEngine.UI;

public class VanUIUpdater : Observer
{

    private VanController vanController;
    private ResultUpdater resultUpdater;

    [Space(3.0f)]
    [Header("Van UI")]
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private Text comboText;
    [SerializeField]
    private Text speedText;
    [SerializeField]
    private Image boosterImage;

    [SerializeField]
    private Animation speedUp;

    private void Start()
    {
        resultUpdater = GetComponentInChildren<ResultUpdater>();
    }

    public override void Notify(Subject subject)
    {
        if (vanController == null && 
            subject.TryGetComponent<VanController>(out VanController _vanController))
        {
            vanController = _vanController;
        }

        // �޺� ������Ʈ
        if (vanController.Combo > 0)
        {
            comboText.text = $"COMBO {vanController.Combo}";
            if (comboText.gameObject.TryGetComponent<Animation>
                (out Animation comboTextAnimation))
            {
                comboTextAnimation.Play();
            }
        }
        else
        {
            comboText.text = "";
        }

        // ü�� UI ������Ʈ
        hpImage.fillAmount = vanController.CurrentHP;
        boosterImage.fillAmount = vanController.BoosterAmount;
    }

    public void SpeedUpdate(float vanBeforeSpeed, float vanStraightSpeed)
    {
        speedText.text = $"{vanStraightSpeed:F0}KM";
    }

    public void BoosterUpdate(float amount)
    {
        boosterImage.fillAmount = amount;
    }

    public void ComboSpeedUpEffect()
    {
        // �޺������� ���ǵ尡 �����ϴ� ���
        speedUp.Play();
        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
    }

    public void SendResultInfo(float firstPosZ, float lastPosZ, float kill)
    {
        resultUpdater.UpdateResultValue(firstPosZ, lastPosZ, kill);
    }

}
