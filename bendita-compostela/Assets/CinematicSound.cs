using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicSound : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> songs;

    public Button button;
    int currentIndex = 0;
    bool firstPlay;

    private void Awake()
    {
        button.onClick.AddListener(ChangeVolume);
    }
    void Start()
    {
        firstPlay = false;
        songs[0].Play();
        StartCoroutine(FirstSong());
    }

    void Update()
    {
        if (firstPlay)
        {
            firstPlay = false;
            StartCoroutine(OtherSongs());
        }
    }

    public void ChangeVolume()
    {
        currentIndex++;
        StartCoroutine(IncreaseVolume()); 
    }
    IEnumerator FirstSong()
    {
        yield return new WaitForSeconds(8f);
        songs[0].Stop();
        firstPlay = true;

    }
    IEnumerator OtherSongs()
    {
        for (int i = 0; i<=6; i++)
        {
            songs[i].Play();
        }
        ChangeVolume();
        yield return new WaitForSeconds(8f);
        ChangeVolume();
        yield return new WaitForSeconds(8f);
    }

    IEnumerator IncreaseVolume()
    {
        for (float i = 0; i <= 4f; i += Time.deltaTime)
        {
            songs[currentIndex].volume = i / 4f;
            songs[currentIndex - 1].volume = 1f - (i / 4f);
            yield return null;
        }
        yield return null;
   
    }

    
}
