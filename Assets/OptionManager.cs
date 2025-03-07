using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;


    public void ActiveOption()
    {
        Time.timeScale = 0;
        optionPanel.SetActive(true);
    }

    public void ExitOption()
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        GameManager.Instance.LoadGameScene();
        ExitOption();
    }

    public void GoMain()
    {
        GameManager.Instance.LoadStartScene();
        ExitOption();
    }
}
