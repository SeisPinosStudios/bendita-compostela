using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] Light2D lightSelected;
    [SerializeField] float minFlickerSpeed, maxFlickerSpeed;
    [SerializeField] float maxIntensity, minIntensity;

    private void Awake()
    {
        StartCoroutine(Flickering());
    }

    IEnumerator Flickering()
    {
        while (true)
        {
            lightSelected.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        }
    }
}
