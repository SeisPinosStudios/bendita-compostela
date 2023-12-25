using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoseScreenManager : MonoBehaviour, IPointerClickHandler
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

    public IEnumerator HideParticles()
    {
        GameObject particles = GameObject.Find("Particles");
        particles.SetActive(false);
        yield return null;
    }
    public IEnumerator DeathSequence()
    {
        yield return StartCoroutine(HideParticles());
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1.0f);
        text.SetActive(true);
    }

    private void OnEnable()
    {

        StartCoroutine(DeathSequence());

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!text.activeSelf) return;
        DebugManager.Instance.DebugLog("Battle", "LoseScreen click");
        GameManager.Instance.RandomizePlayer();
        GameManager.Instance.ClearProgress();
        SceneManagementUtils.StaticLoadScene("MainMenu");
    }
}
