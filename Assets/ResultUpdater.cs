using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpdater : MonoBehaviour
{
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text killText;
    [SerializeField]
    private float distanceIncreaseAmount;
    [SerializeField]
    private float killZombieIncreaseAmount;
    [SerializeField]
    private float distanceReduceAmount;
    [SerializeField]
    private float killZombieReduceAmount;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private float moneyIncreaseAmount;

    [SerializeField]
    private Button acceptButton;
    [SerializeField]
    private Button acceptX2Button;

    private float moveDistance;
    private float killZombieNum;

    private float distanceUIValue;
    private float killUIValue;
    private float finalMoneyValue;


    private Animation animation;
    [SerializeField]
    private float gameOverDownTime = 3.0f;

    private Vector3 firstUIPos;


    private void Awake()
    {
        firstUIPos = transform.position;

        animation = GetComponent<Animation>();
        animation["GameOver"].speed = 1 / gameOverDownTime;
    }


    public void UpdateResultValue()
    {
        moveDistance = GameManager.Instance.playerMoveDistance;
        killZombieNum = GameManager.Instance.playerKill;
        GameOver();
        Debug.Log("GameOver Panel Down");
    }
    public void GameOver()
    {
        StartCoroutine(StartGameOver());
    }

    IEnumerator StartGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        animation.Play();
    }
    public void GameOverResultUI()
    {
        StartCoroutine(IncreaseValue());
    }

    private IEnumerator IncreaseValue()
    {
        while (!(distanceUIValue == moveDistance && killUIValue == killZombieNum))
        {

            distanceUIValue += distanceIncreaseAmount;
            killUIValue += killZombieIncreaseAmount;

            if (distanceUIValue >= moveDistance)
                distanceUIValue = moveDistance;
            if (killUIValue >= killZombieNum)
                killUIValue = killZombieNum;



            yield return null;
            distanceText.text = $"{distanceUIValue:F0}M";
            killText.text = $"{killUIValue:F0}";
        }
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(CalculateMoney());
    }

    private IEnumerator CalculateMoney()
    {
        while (distanceUIValue != 0 || killUIValue != 0)
        {
            if (distanceUIValue > 0)
            {

                distanceUIValue -= distanceReduceAmount;
            }
            else
            {
                distanceUIValue = 0;
            }

            if (killUIValue > 0)
            {

                killUIValue -= killZombieReduceAmount;

            }
            else
            {
                killUIValue = 0;
            }

            distanceText.text = $"{distanceUIValue:F0}M";
            killText.text = $"{killUIValue:F0}";


            finalMoneyValue += moneyIncreaseAmount;
            moneyText.text = $"{finalMoneyValue:F0}";

            yield return null;
        }

        //버튼 표시
        acceptButton.gameObject.SetActive(true);
        acceptX2Button.gameObject.SetActive(true);
    }


    public void AcceptResult()
    {
        DeActivateButtons();
        DataManager.Instance.GetMoneyData(finalMoneyValue);
        DataManager.Instance.UpdateMoney();
        ReturnUIOrigin();
        GameManager.Instance.LoadStartScene();
    }

    public void AcceptX2()
    {
        DeActivateButtons();
        DataManager.Instance.GetMoneyData(finalMoneyValue);
        ReturnUIOrigin();
        AdsManager.Instance.rewardedAds.ShowAd();
    }

    private void ReturnUIOrigin()
    {
        transform.position = firstUIPos;
        moneyText.text = "0";
        finalMoneyValue = 0;
        animation.Rewind();
    }
    private void DeActivateButtons()
    {
        acceptButton.gameObject.SetActive(false);
        acceptX2Button.gameObject.SetActive(false);
    }

}
