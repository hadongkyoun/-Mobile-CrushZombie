using System;
using System.IO;
using UnityEngine;



[System.Serializable]
public class PlayerDatas
{
    public int needUpgradeGold_speed = 100;
    public int lv_speed = 1;

    public int needUpgradeGold_durability = 100;
    public int lv_durability = 1;

    public int needUpgradeGold_offline = 100;
    public int lv_offline = 1;

    // VanData �ʱⰪ
    public float acceleration = 3.5f;
    public float maxVanHP = 20;
    public float increaseMoneyOffline = 0f;

    public int money;
    public string lastPlayTime = "None";

}


public class DataManager : Singleton<DataManager>
{

    public PlayerDatas playerData;
    private string path;
    [SerializeField]
    private string fileName = "/save.txt";
    private string keyWord = "sadiqwjwkd#dkwda!! ej2kS@@";

    private float moneyTemp;
    public bool isRewarded;
    private int offlineRewardMoney;


    public void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            SaveData();
            Debug.Log("Focus lost");
        }
        else
        {
            LoadData();
            UpdateRewardMoney();

            Debug.Log("Focus come");
        }
    }

    //public void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        playerData.lastPlayTime = DateTime.Now;
    //    }
    //}

    public void UpdateRewardMoney()
    {
        if (String.Equals(playerData.lastPlayTime, "None"))
        {
            Debug.Log("There is no last play time");
            return;
        }

        DateTime lastPlayTime = DateTime.Parse(playerData.lastPlayTime);
        TimeSpan elapsedTime = (DateTime.Now).Subtract(lastPlayTime);
        float calculateElapsedTime = (float)elapsedTime.TotalMinutes / 2;
        Debug.Log(calculateElapsedTime);
        offlineRewardMoney = (int)calculateElapsedTime;

        if (offlineRewardMoney > 0)
        {
            playerData.money += offlineRewardMoney;
            UIManager.Instance.ActivateRewardPanel(offlineRewardMoney);
        }
    }

    public void SaveData()
    {
        playerData.lastPlayTime = DateTime.Now.ToString("O");
        Debug.Log(DateTime.Now);

        path = Application.persistentDataPath + fileName;
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, EncryptAndDecrypt(data));

        Debug.Log("Save Success");

    }

    public bool LoadData()
    {
        path = Application.persistentDataPath + fileName;

        if (!File.Exists(path))
        {
            SaveData();
        }
        string data = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerDatas>(EncryptAndDecrypt(data));
        Debug.Log("LoadSuccess");

        return true;
    }

    private string EncryptAndDecrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }

        return result;
    }


    public void UpgradeSpeedData()
    {
        if (playerData.money < playerData.needUpgradeGold_speed)
            return;
        else
        {
            playerData.acceleration += 0.5f;
            playerData.money -= playerData.needUpgradeGold_speed;
            playerData.needUpgradeGold_speed += 10;
            playerData.lv_speed++;

        }

        SaveData();
    }
    public void UpgradeDurabilityData()
    {
        if (playerData.money < playerData.needUpgradeGold_durability)
            return;
        else
        {
            playerData.maxVanHP += 2.0f;
            playerData.money -= playerData.needUpgradeGold_durability;
            playerData.needUpgradeGold_durability += 10;
            playerData.lv_durability++;
        }

        SaveData();
    }

    public void UpgradeOfflineMoneyData()
    {
        if (playerData.money < playerData.needUpgradeGold_offline)
            return;
        else
        {
            playerData.increaseMoneyOffline += 0.2f;
            playerData.money -= playerData.needUpgradeGold_offline;
            playerData.needUpgradeGold_offline += 10;
            playerData.lv_offline++;
        }


        SaveData();
    }

    public void UpdateMoney()
    {
        if (isRewarded)
            playerData.money += (int)moneyTemp * 2;
        else
            playerData.money += (int)moneyTemp;

        UIManager.Instance.UpdateMoneyText(playerData.money);
        SaveData();

        isRewarded = false;
    }

    public void GetMoneyData(float _money)
    {
        moneyTemp = _money;
    }

    public void SetVanData(VanData vanData)
    {
        vanData.MaxVanHP = playerData.maxVanHP;
    }

}
