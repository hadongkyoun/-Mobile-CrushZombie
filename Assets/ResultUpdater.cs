using System.Collections;
using System.Runtime.CompilerServices;
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


    public void UpdateResultValue(float firstPosZ, float lastPosZ, float _kill)
    {
        moveDistance = (lastPosZ - firstPosZ)/10;
        killZombieNum = _kill;
        GameOver();
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
    }

    public void AcceptResult()
    {
        acceptButton.gameObject.SetActive(false);
        transform.position = firstUIPos;
        DataManager.Instance.UpdateMoney(finalMoneyValue);
        GameManager.Instance.LoadStartScene();
    }

}
