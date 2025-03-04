
using System.Diagnostics;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    
    public void LoadStartScene()
    {
        DataManager.Instance.SaveData();
       
        SceneManager.LoadScene("StartScene");
        LoadUI(SceneManager.GetSceneByName("StartScene").buildIndex);
    }

    public void LoadGameScene()
    {
        DataManager.Instance.LoadData();

        SceneManager.LoadScene("GameScene");
        LoadUI(SceneManager.GetSceneByName("GameScene").buildIndex);
    }

    private void LoadUI(int index)
    {
        UIManager.Instance.LoadCurrentSceneUI(index);
    }

}
