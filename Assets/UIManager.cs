
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject gameSceneUI;
    [SerializeField]
    private GameObject startSceneUI;
    [SerializeField]
    private GameObject offlineRewardUI;
    [SerializeField]
    private Text offlineRewardText;

    [Tooltip("0 => Lv, 1 => need Gold")]
    [Space(20)]
    [SerializeField]
    private Text[] upgradeTexts;
    [SerializeField]
    private Text[] upgradeCarTexts;
    [SerializeField]
    private Text[] upgradeOfflineTexts;

    [SerializeField]
    private Text money;

    [SerializeField]
    private ClickAnimationHandler[] clickAnimationHandlers;

    public bool init = false;

    [SerializeField]
    private Animation speedUpAnimation;

    [SerializeField]
    private Text time;

    [SerializeField]
    private Text comboText;

    public void UpdatePlayTime(float playTimeSecond)
    {
        time.text = $"{playTimeSecond:F0}";
    }

    public void ResetComboText()
    {
        comboText.text = "";
    }
    public void UpdateComboText(int combo)
    {
        comboText.text = $"Combo {combo}";
        if(comboText.TryGetComponent<Animation>(out Animation animation))
        {
            animation.Play();
        }
    }

    public void ActivateSpeedUpAnim()
    {
        speedUpAnimation.Play();
    }

    public void ActivateRewardPanel(int _money)
    {
        offlineRewardUI.SetActive(true);
        offlineRewardText.text = $"{_money}";
    }

    public void DeActivateRewardPanel()
    {
        offlineRewardUI.SetActive(false);
    }

    public void UpdateMoneyText(float _money)
    {
        money.text = $"{_money}";
    }

    public void UpdateSpeedLevelAndMoney(int lv, int needGold)
    {
        upgradeTexts[0].text = $"Lv {lv}";
        upgradeTexts[1].text = $"{needGold}";

        if (init)
        {
            if (clickAnimationHandlers[0] != null)
                clickAnimationHandlers[0].PlayAnim();
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
        }
    }
    public void UpdateCarHpLevelAndMoney(int lv, int needGold)
    {
        upgradeCarTexts[0].text = $"Lv {lv}";
        upgradeCarTexts[1].text = $"{needGold}";

        if (init)
        {
            if (clickAnimationHandlers[1] != null)
                clickAnimationHandlers[1].PlayAnim();
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
        }
    }

    public void UpdateOfflineRewardsLevelAndMoney(int lv, int needGold)
    {
        upgradeOfflineTexts[0].text = $"Lv {lv}";
        upgradeOfflineTexts[1].text = $"{needGold}";

        if (init)
        {
            if (clickAnimationHandlers[2] != null)
                clickAnimationHandlers[2].PlayAnim();
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
        }
    }


    public void LoadCurrentSceneUI(int currentSceneIndex)
    {
        if (currentSceneIndex == 0)
            LoadStartSceneUI();
        else if (currentSceneIndex == 1)
            LoadGameSceneUI();
    }

    private void LoadStartSceneUI()
    {


        gameSceneUI.SetActive(false);

        startSceneUI.SetActive(true);

        money.transform.parent.gameObject.SetActive(true);

        UpdateAllStartSceneUI();
    }

    public void UpdateAllStartSceneUI()
    {
        UpdateMoneyText(DataManager.Instance.playerData.money);
        UpdateSpeedLevelAndMoney(DataManager.Instance.playerData.lv_speed, DataManager.Instance.playerData.needUpgradeGold_speed);
        UpdateCarHpLevelAndMoney(DataManager.Instance.playerData.lv_durability, DataManager.Instance.playerData.needUpgradeGold_durability);
        UpdateOfflineRewardsLevelAndMoney(DataManager.Instance.playerData.lv_offline, DataManager.Instance.playerData.needUpgradeGold_offline);
    }

    private void LoadGameSceneUI()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_BUTTON);
        gameSceneUI.SetActive(true);
        startSceneUI.SetActive(false);
        money.transform.parent.gameObject.SetActive(false);

    }

}
