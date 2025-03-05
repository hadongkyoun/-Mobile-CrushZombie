using System.Collections.Generic;
using UnityEngine;




public class AudioManager : Singleton<AudioManager>
{
    public enum BGM
    {
        BGM_TITLE,
        BGM_INGAME,
    }

    public enum SFX
    {
        SFX_BUTTON,
        SFX_ZOMBIE,
        SFX_TREE,
        SFX_BARREL,
        SFX_ROCK,
        SFX_UPGRADE,
        SFX_CARCRASH,
    }


    [SerializeField]
    private AudioClip[] bgms;
    [SerializeField]
    private AudioClip[] sfxs;

    [SerializeField]
    private AudioSource audioBgm;
    [SerializeField]
    private AudioSource audioSfx;
    
    public void PlayBGM(BGM bgmIndex)
    {
        audioBgm.clip = bgms[(int)bgmIndex];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(SFX sfxIndex)
    {
        audioSfx.PlayOneShot(sfxs[(int)sfxIndex]);
    }




}
