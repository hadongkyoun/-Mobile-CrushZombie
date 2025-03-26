using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private float playTime = 90;
    public float GetPlayTime { get { return playTime; } set { playTime = value; } }

    public float playerKill;
    public float playerMoveDistance;

    public bool playerDead;

    public void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        playTime -= Time.deltaTime;
    }


    public void LoadStartScene()
    {
        playerKill = 0;
        playerMoveDistance = 0;
        playerDead = false;
        SceneManager.LoadScene("StartScene");

        DataManager.Instance.SaveData();
        AudioManager.Instance.PlayBGM(AudioManager.BGM.BGM_TITLE);
    }

    public void LoadGameScene()
    {

        //DataManager.Instance.LoadData();

        UIManager.Instance.init = true;
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.BGM_INGAME);
    }


}
