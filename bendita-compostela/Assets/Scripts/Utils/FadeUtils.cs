using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeUtils : MonoBehaviour
{
    public float fadeDuration; // Duración de la transición en segundos
    [SerializeField] private Image imageComponent;
    [SerializeField] private bool isFading = false;

    // Evento que se dispara cuando la fade ha terminado
    public event Action OnFadeComplete;

    // Método para realizar un fade in (aparecer gradualmente)
    public void FadeIn()
    {        
        if (!isFading)
        {
            StartCoroutine(FadeToAlpha(1.0f, () => {
                // Llamamos a la función de devolución de llamada cuando la fade ha terminado
                if (OnFadeComplete != null)
                    OnFadeComplete.Invoke();
            }));
        }
    }

    // Método para realizar un fade out (desvanecer gradualmente)
    public void FadeOut()
    {        
        if (!isFading)
        {
            StartCoroutine(FadeToAlpha(0.0f, () => {
                // Llamamos a la función de devolución de llamada cuando la fade ha terminado
                if (OnFadeComplete != null)
                    OnFadeComplete.Invoke();
            }));
        }
    }

    // Corrutina para realizar la transición de transparencia
    private IEnumerator FadeToAlpha(float targetAlpha, Action onComplete)
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

        imageComponent.color = targetColor; // Asegurarse de que la transición termine en el valor objetivo
        isFading = false;

        // Llamamos a la función de devolución de llamada cuando la fade ha terminado
        if (onComplete != null)
            onComplete.Invoke();
    }
}
