using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeUtils : MonoBehaviour
{    
    [SerializeField] private Image imageComponent;
    [SerializeField] private bool isFading = false;

    public event Action OnFadeComplete;    
    public void FadeIn(float fadeDuration)
    {        
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
        if (!isFading)
        {
            StartCoroutine(FadeToAlpha(0.0f, () => {
                
                if (OnFadeComplete != null)
                    OnFadeComplete.Invoke();
            }
            ,fadeDuration));
        }
    }

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
        
        if (onComplete != null)
            onComplete.Invoke();
    }
}
