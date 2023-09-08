using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeUtils : MonoBehaviour
{    
    [SerializeField] private Image imageComponent;
    [SerializeField] private SpriteRenderer rendererComponent;
    [SerializeField] private bool isFading = false;

    [Header("Intermitent Sprite Fade")]
    [SerializeField] private bool loopFade = false;
    [SerializeField] private float loopFadeSpeed = 0.5f;

    public event Action OnFadeComplete;

    Coroutine fadeLoopCoroutine;

    #region Image Fade with Finish Event
    public void FadeIn(float fadeDuration)
    {
        if (!imageComponent.gameObject.activeSelf) imageComponent.gameObject.SetActive(true);
        if (!isFading)
        {
            StartCoroutine(FadeToAlpha(1.0f, () => {                
                if (OnFadeComplete != null)
                    OnFadeComplete.Invoke();
            }
            ,fadeDuration));
        }
    }
    public void FadeOut(float fadeDuration)
    {
        if (!imageComponent.gameObject.activeSelf) imageComponent.gameObject.SetActive(true);
        if (!isFading)
        {
            StartCoroutine(FadeToAlpha(0.0f, () => {
                
                if (OnFadeComplete != null)
                    OnFadeComplete.Invoke();
            }
            ,fadeDuration));
        }
    }
    private IEnumerator FadeToAlpha(float targetAlpha, Action onComplete,float fadeDuration)
    {        
        isFading = true;
        Color startColor = imageComponent.color;
        Color targetColor = startColor;
        targetColor.a = targetAlpha;

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            imageComponent.color = Color.Lerp(startColor, targetColor, normalizedTime);            

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        imageComponent.color = targetColor;
        isFading = false;

        if(targetAlpha <= 0f) imageComponent.gameObject.SetActive(false);

        if (onComplete != null)
            onComplete.Invoke();
    }
    #endregion

    #region Image Fade 
    public IEnumerator FadeOutCoroutine(float fadeTime)
    {
        if (!imageComponent.gameObject.activeSelf) imageComponent.gameObject.SetActive(true);
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            imageComponent.color = new Color(0f, 0f, 0f, i / fadeTime);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator FadeInCoroutine(float fadeTime)
    {
        if (!imageComponent.gameObject.activeSelf) imageComponent.gameObject.SetActive(true);
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            imageComponent.color = new Color(0f, 0f, 0f, i / fadeTime);
            yield return null;
        }

        yield return null;
    }
    #endregion    
    
    #region Sprite Continous Fade
    private void Start() 
    {
        if(loopFade && rendererComponent != null) StartLoopFade();
    }
    public void StartLoopFade()
    {
        fadeLoopCoroutine = StartCoroutine(FadeInAndOut(loopFadeSpeed));
    }
    public void StopFade() 
    {
        if(fadeLoopCoroutine != null)StopCoroutine(fadeLoopCoroutine);
    }
    private IEnumerator FadeInAndOut(float fadeSpeed)
    {
        while (true)
        {            
            yield return FadeTo(0f,fadeSpeed);            
            yield return FadeTo(1f,fadeSpeed);            
        }
    }
    private IEnumerator FadeTo(float targetAlpha, float fadeSpeed)
    {
         Color startColor = rendererComponent.color;
        Color targetColor = startColor;
        targetColor.a = targetAlpha;

        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            float normalizedTime = elapsedTime / fadeSpeed;
            rendererComponent.color = Color.Lerp(startColor, targetColor, normalizedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rendererComponent.color = targetColor;
    }
    #endregion
    

    
}
