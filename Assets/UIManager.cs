
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject gameSceneUI;
    [SerializeField]
    private GameObject startSceneUI;


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

    public void UpdateMoneyText(float _money)
    {
        money.text = $"{_money}";
    }

    public void UpdateBoosterSpeedLevelAndMoney(int lv, int needGold)
    {
        upgradeBoosterTexts[0].text = $"Lv {lv}";
        upgradeBoosterTexts[1].text = $"{needGold}";
    }
    public void UpdateCarHpLevelAndMoney(int lv, int needGold)
    {
        upgradeCarTexts[0].text = $"Lv {lv}";
        upgradeCarTexts[1].text = $"{needGold}";
    }

    public void UpdateOfflineRewardsLevelAndMoney(int lv, int needGold)
    {
        upgradeOfflineTexts[0].text = $"Lv {lv}";
        upgradeOfflineTexts[1].text = $"{needGold}";
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
    }

    private void LoadGameSceneUI()
    {
        startSceneUI.SetActive(false);
        gameSceneUI.SetActive(true);
        money.transform.parent.gameObject.SetActive(false);
    }

}
