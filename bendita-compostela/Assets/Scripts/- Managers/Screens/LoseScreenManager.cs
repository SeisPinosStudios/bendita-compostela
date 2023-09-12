using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenManager : MonoBehaviour
{
    [field: SerializeField] public Image backgroundImage { get; private set; }
    [field: SerializeField] public float fadeTime { get; private set; }
    [field: SerializeField] public GameObject text { get; private set; }

    public IEnumerator FadeOut()
    {
        for(float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            backgroundImage.color = new Color(0, 0, 0, i);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator DeathSequence()
    {
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1.0f);
        text.SetActive(true);
    }

    private void OnEnable()
    {
        StartCoroutine(DeathSequence());
    }
}
