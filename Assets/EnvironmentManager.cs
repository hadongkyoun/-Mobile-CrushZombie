using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField]
    private Transform vanTransform;

    [SerializeField]
    private Transform[] Sections;
    [SerializeField]
    private float moveOffSet = 60.0f;
    [SerializeField]
    private float standardOffSet = 10.0f;

    private int sectionIndex = 0;


    private bool moveTrigger = false;
    
    // Update is called once per frame
    void Update()
    {
        // 기준이 되는 Section
        if(vanTransform != null && vanTransform.position.z >= 
                                Sections[(sectionIndex + 1)%Sections.Length].position.z + standardOffSet)
        {

            moveTrigger = true;
            
        }


        if (moveTrigger)
        {
            // 이동하는 Section
            Sections[sectionIndex % Sections.Length].position += new Vector3(0, 0, moveOffSet);
            sectionIndex += 1;
            moveTrigger = false;
        }
    }

}
