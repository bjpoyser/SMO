using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource nextAudio, backAudio, selectAudio, optionAudio;

    public void PlayNextAudio()
    {
        nextAudio.Play();
    }

    public void PlaySelectAudio()
    {
        selectAudio.Play();
    }

    public void PlayBackAudio()
    {
        backAudio.Play();
    }

    public void PlayOptionAudio()
    {
        optionAudio.Play();
    }
}
