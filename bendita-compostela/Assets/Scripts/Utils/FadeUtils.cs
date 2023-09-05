using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUtils : MonoBehaviour
{
    [field: SerializeField] public Image fadeImage { get; private set; }

    public IEnumerator FadeOut(float fadeTime)
    {
        if (!fadeImage.gameObject.activeSelf) fadeImage.gameObject.SetActive(true);
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            fadeImage.color = new Color(0f, 0f, 0f, i / fadeTime);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator FadeIn(float fadeTime)
    {
        if (!fadeImage.gameObject.activeSelf) fadeImage.gameObject.SetActive(true);
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            fadeImage.color = new Color(0f, 0f, 0f, i / fadeTime);
            yield return null;
        }

        yield return null;
    }

}

