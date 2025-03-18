using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    //public void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //    {
    //        LoadStartScene();
    //    }
    //}

    public void LoadStartScene()
    {

        SceneManager.LoadScene("StartScene");
        LoadUI(SceneManager.GetSceneByName("StartScene").buildIndex);

        DataManager.Instance.SaveData();
        AudioManager.Instance.PlayBGM(AudioManager.BGM.BGM_TITLE);
    }

    public void LoadGameScene()
    {
        UIManager.Instance.init = true;
        SceneManager.LoadScene("GameScene");
        LoadUI(SceneManager.GetSceneByName("GameScene").buildIndex);
        AudioManager.Instance.PlayBGM(AudioManager.BGM.BGM_INGAME);
    }

    private void LoadUI(int index)
    {
        UIManager.Instance.LoadCurrentSceneUI(index);
    }

}
