using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private Sound mainMenuMusic;
    private void Awake() {
        SoundManager.Instance.PlayMusic(mainMenuMusic.AudioClip, mainMenuMusic.Volume);
        
        ////////////////////////////////////////////// MUSICA /////////////////////////////////////
        MusicManager.Instance.Parar();
        MusicManager.Instance.PlayMusic("event:/menu");
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => SoundManager.Instance);
        SoundManager.Instance.PlayMusic(mainMenuMusic.AudioClip, mainMenuMusic.Volume);
    }

    public void Play()
    {
        SceneManager.LoadScene("Map");
    }
    public void Settings()
    {
        Instantiate(SettingsCanvas);
    }
    public void Credits()
    {
        //credits
    }
    public void Exit()
    {
        Application.Quit();
    }
}
