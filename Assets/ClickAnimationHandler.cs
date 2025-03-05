using UnityEngine;

public class ClickAnimationHandler : MonoBehaviour
{
    [SerializeField]
    private Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }


    public void PlayAnim()
    {
        if (animation.isPlaying)
        {
            animation.Stop();
        }

        animation.Play();
    }
}
