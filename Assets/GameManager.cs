using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    public void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            LoadLoadingScene();
        }
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
        LoadUI(SceneManager.GetSceneByName("LoadingScene").buildIndex);
    }

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

        DataManager.Instance.LoadData();

        SceneManager.LoadScene("GameScene");
        LoadUI(SceneManager.GetSceneByName("GameScene").buildIndex);
        AudioManager.Instance.PlayBGM(AudioManager.BGM.BGM_INGAME);
    }

    private void LoadUI(int index)
    {
        UIManager.Instance.LoadCurrentSceneUI(index);
    }

}
