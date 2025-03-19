using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlin;

    [SerializeField]
    private float shakingStayTime = 0.5f;


    IEnumerator CameraShake()
    {
        if(perlin == null)
        {
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        } 

        perlin.m_AmplitudeGain = 1;
        yield return new WaitForSecondsRealtime(shakingStayTime);
        perlin.m_AmplitudeGain = 0;
    }

    public void Shake()
    {
        StartCoroutine(CameraShake());
    }
}
