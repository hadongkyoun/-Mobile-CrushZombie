using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI testObject;
    
    void Awake()
    {
        testObject.text = "Nothing";   
    }

}
