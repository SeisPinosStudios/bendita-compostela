using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUtils : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField] float fadeDuration = 1.0f;

    private bool isFadingOut = false;

    
    private void OnEnable() {
        StartCoroutine(FadeInOut());
    }
    private IEnumerator FadeInOut()
    {
        while (true)
        {
            if (isFadingOut)
            {
                // Cambiar la dirección de la transición cuando la imagen esté completamente visible
                while (imageComponent.color.a >= 1.0f)
                {
                    yield return null;
                }
                isFadingOut = false;
            }
            else
            {
                // Hacer completamente visible
                while (imageComponent.color.a < 1.0f)
                {
                    float alpha = Mathf.Min(imageComponent.color.a + Time.deltaTime / fadeDuration, 1.0f);
                    Color newColor = imageComponent.color;
                    newColor.a = alpha;
                    imageComponent.color = newColor;
                    yield return null;
                }
                isFadingOut = true;
            }
            
            // Hacer completamente transparente
            while (imageComponent.color.a > 0.0f)
            {
                float alpha = Mathf.Max(imageComponent.color.a - Time.deltaTime / fadeDuration, 0.0f);
                Color newColor = imageComponent.color;
                newColor.a = alpha;
                imageComponent.color = newColor;
                yield return null;
            }
        }

    }
}
