using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Text[] upgradeTexts;
    [SerializeField]
    private Text[] upgradeCarTexts;
    [SerializeField]
    private Text[] upgradeOfflineTexts;


    public void UpgradeSpeedData()
    {
        DataManager.Instance.UpgradeSpeedData();
        UpdateSpeedLevelAndMoney(DataManager.Instance.playerData.lv_speed, DataManager.Instance.playerData.needUpgradeGold_speed);
    }
    public void UpgradeDurabilityData()
    {
        DataManager.Instance.UpgradeDurabilityData();
        UpdateCarHpLevelAndMoney(DataManager.Instance.playerData.lv_durability, DataManager.Instance.playerData.needUpgradeGold_durability);
    }

    public void UpgradeOfflineMoneyData()
    {
        DataManager.Instance.UpgradeOfflineMoneyData();
        UpdateOfflineRewardsLevelAndMoney(DataManager.Instance.playerData.lv_offline, DataManager.Instance.playerData.needUpgradeGold_offline);
    }


    public void UpdateSpeedLevelAndMoney(int lv, int needGold)
    {
        upgradeTexts[0].text = $"Lv {lv}";
        upgradeTexts[1].text = $"{needGold}";
        moneyText.text = $"{DataManager.Instance.playerData.money}";

    }
    public void UpdateCarHpLevelAndMoney(int lv, int needGold)
    {
        upgradeCarTexts[0].text = $"Lv {lv}";
        upgradeCarTexts[1].text = $"{needGold}";
        moneyText.text = $"{DataManager.Instance.playerData.money}";

    }

    public void UpdateOfflineRewardsLevelAndMoney(int lv, int needGold)
    {
        upgradeOfflineTexts[0].text = $"Lv {lv}";
        upgradeOfflineTexts[1].text = $"{needGold}";
        moneyText.text = $"{DataManager.Instance.playerData.money}";
    }
}
