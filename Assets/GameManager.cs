using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    public void OnApplicationQuit()
    {
        DataManager.Instance.SaveData();
        DataManager.Instance.SaveLastTime(DateTime.Now);
        Debug.Log("Save");
    }

    public void LoadStartScene()
    {
        DataManager.Instance.SaveData();
       
        SceneManager.LoadScene("StartScene");
        LoadUI(SceneManager.GetSceneByName("StartScene").buildIndex);
        
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
