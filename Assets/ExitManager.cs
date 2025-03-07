using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public void Exit()
    {
        DataManager.Instance.SaveData();
        Application.Quit();
    }
}
