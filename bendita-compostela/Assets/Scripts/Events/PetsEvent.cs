using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsEvent : MonoBehaviour
{
    #region Variables
    [SerializeField] FadeUtils fader;
    [SerializeField] Animator fishAnimator;
    [SerializeField] GameObject[] cats;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject rewardWindow;
    
    [Header("Sounds")]
    [SerializeField] Sound catsSound;
    [SerializeField] Sound catHappySound;
    [SerializeField] Sound catsMusic;
    #endregion

    #region Rewards DIEGO AQUI  
    public void Feed()
    {   
        // SI ALIMENTAS AL GATO
        OpenRewardWindow("Condecoración Fiel Ameowgo");
        SoundManager.Instance.PlayMusic(catHappySound.AudioClip, catHappySound.Volume);
    }
    public void Throw()
    {
        // SI LO TIRAS AL RIO
        OpenRewardWindow("Obtienes condecoración Orden de Samael");
    }
    #endregion


    #region Event Logic
    private void Awake() 
    {
        SoundManager.Instance.PlayMusic(catsMusic.AudioClip,catsMusic.Volume);
    }

    private void Start() 
    {        
        fader.FadeOut(1.0f);
        fader.OnFadeComplete += FishMoves;
    }
    private void FishMoves()
    {
        fishAnimator.SetTrigger("Iddle");        
        Invoke("CatsAppear", 2.0f);
        fader.OnFadeComplete -= FishMoves;
    }
    private void CatsAppear()
    {
        fader.FadeIn(1.0f);
        fader.OnFadeComplete += Stop; 
        
    }
    void Stop()
    {
        Invoke("FadeTransition",1.0f);
        SoundManager.Instance.PlaySound(catsSound.AudioClip, catsSound.Volume);
        fader.OnFadeComplete -= Stop; 
    }
    private void FadeTransition()
    {                
        foreach (GameObject cat in cats)
        {
            cat.SetActive(true);
        }        
        fader.FadeOut(1.0f);
        
        fader.OnFadeComplete += FinalScene; 
    }
    private void FinalScene()
    {
        fader.OnFadeComplete -= FinalScene; 
        buttons.SetActive(true);        
    }


    private void OpenRewardWindow(string textReward)
    {
        fishAnimator.gameObject.SetActive(false);
        rewardWindow.SetActive(true);
        rewardWindow.GetComponent<EventRewardDisplay>().SetTextReward(textReward);
    }
    public void EndEvent()
    {
        Destroy(gameObject);
    }
    #endregion
}


