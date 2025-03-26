
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject offlineRewardUI;
    [SerializeField]
    private Text offlineRewardText;


    [SerializeField]
    private Text money;

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



}
