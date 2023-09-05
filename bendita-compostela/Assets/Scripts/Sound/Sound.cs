using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Sound 
{
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
}
