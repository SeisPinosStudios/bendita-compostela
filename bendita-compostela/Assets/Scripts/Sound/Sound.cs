using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Sound 
{
    [field: SerializeField] public string soundName { get; private set; }
    [Header("Sound Config")]
    [SerializeField] private AudioClip audioClip;

    [Range(0.0f,1.0f)]
    [SerializeField] private float volume;

    public AudioClip AudioClip
    {
        get {return audioClip;}
    }
    public float Volume
    {
        get{return volume;}
    }
    public void PlaySound() 
    {
        SoundManager.Instance.PlaySound(audioClip,volume);
    }
    public void PlayMusic() 
    {
        SoundManager.Instance.PlayMusic(this);
    }
}
