using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixerGroup mixer;
    
    public void SetLevel (float value)
    {
        string name = mixer.name + "Vol";
        mixer.audioMixer.SetFloat(name, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(name, value);
    }
}
