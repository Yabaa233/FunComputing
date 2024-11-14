using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip BGM;
    public AudioClip Over;
    public AudioClip Sticker;
    public AudioClip Right;

    public AudioSource BGMSource;
    public AudioSource OtherSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void GameStart()
    {
        PlayBGM();
    }
    private void PlayBGM()
    {
        if (!BGMSource.isPlaying) 
        {
            BGMSource.Play();
        }

    }
    private void StopBGM()
    {
        BGMSource.Stop();
    }
    public void GameOver()
    {
        StopBGM();
        OtherSource.clip = Over;
        OtherSource.Play();
    }
    public void RightAction()
    {
        OtherSource.clip = Right;
        OtherSource.Play();
    }
    public void GetSticker()
    {
        OtherSource.clip = Sticker;
        OtherSource.Play();
    }
}
