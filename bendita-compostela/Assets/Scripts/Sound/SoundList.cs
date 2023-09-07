using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    [Header("Lista de Musica")]
    [SerializeField] public List<Sound> musicList = new List<Sound>();
    [Header("Lista de Efectos de Sonido")]
    [SerializeField] public List<Sound> vfxSoundsList = new List<Sound>();

    public void PlaySound(string soundName) 
    {
        vfxSoundsList.Find(sound => sound.soundName == soundName).PlaySound();
    }
    public void PlayMusic(string soundName)
    {
        musicList.Find(sound => sound.soundName == soundName).PlayMusic();
    }

}
