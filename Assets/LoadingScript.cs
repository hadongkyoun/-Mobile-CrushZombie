using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    private bool checkDataLoads = true;
    private bool loading = true;

    [SerializeField]
    private Slider slider;

    public void Init()
    {
        loading = true;
        checkDataLoads = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (checkDataLoads)
        {
            slider.value = 0;
            StartCoroutine(CheckData());
        }

        if (loading)
        {
            slider.value += Time.deltaTime / 3.0f;
            Debug.Log("loading :" + loading);
        }
        else
        {
            slider.value = 1.0f;
            GameManager.Instance.LoadStartScene();
        }
    }

    IEnumerator CheckData()
    {
        checkDataLoads = false;
        bool success = false;
        success = DataManager.Instance.LoadData();
        if (success)
        {
            yield return null;
            loading = false;
        }
    }
}
