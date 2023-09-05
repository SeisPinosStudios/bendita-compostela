using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource, effectsSource; 
    

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        effectsSource.PlayOneShot(clip,volume);
    }
    public void PlayMusic(AudioClip clip)
    {                
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.PlayOneShot(clip);
        
    }
    public void PlayMusic(AudioClip clip, float volume)
    {                
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.PlayOneShot(clip,volume);
        
    }
}