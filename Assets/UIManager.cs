
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject loadingSceneUI;
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
    private Text[] upgradeBoosterTexts;
    [SerializeField]
    private Text[] upgradeCarTexts;
    [SerializeField]
    private Text[] upgradeOfflineTexts;

    [SerializeField]
    private Text money;

    [SerializeField]
    private ClickAnimationHandler[] clickAnimationHandlers;

    public bool init = false;

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

    public void UpdateBoosterSpeedLevelAndMoney(int lv, int needGold)
    {
        upgradeBoosterTexts[0].text = $"Lv {lv}";
        upgradeBoosterTexts[1].text = $"{needGold}";

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
            LoadLoadingSceneUI();
        else if (currentSceneIndex == 1)
            LoadStartSceneUI();
        else
            LoadGameSceneUI();
    }

    private void LoadStartSceneUI()
    {


        gameSceneUI.SetActive(false);
        loadingSceneUI.SetActive(false);
        startSceneUI.SetActive(true);

        money.transform.parent.gameObject.SetActive(true);

        UpdateMoneyText(DataManager.Instance.playerData.money);
        UpdateBoosterSpeedLevelAndMoney(DataManager.Instance.playerData.lv_speed, DataManager.Instance.playerData.needUpgradeGold_speed);
        UpdateCarHpLevelAndMoney(DataManager.Instance.playerData.lv_durability, DataManager.Instance.playerData.needUpgradeGold_durability);
        UpdateOfflineRewardsLevelAndMoney(DataManager.Instance.playerData.lv_offline, DataManager.Instance.playerData.needUpgradeGold_offline);
    }

    private void LoadGameSceneUI()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_BUTTON);
        startSceneUI.SetActive(false);
        loadingSceneUI.SetActive(false);
        gameSceneUI.SetActive(true);
        money.transform.parent.gameObject.SetActive(false);

    }

    private void LoadLoadingSceneUI()
    {

        startSceneUI.SetActive(false);
        gameSceneUI.SetActive(false);
        loadingSceneUI.SetActive(true);
        if(loadingSceneUI.TryGetComponent<LoadingScript>(out LoadingScript loadingScript))
        {
            loadingScript.Init();
        }
        money.transform.parent.gameObject.SetActive(false);
    }

}
