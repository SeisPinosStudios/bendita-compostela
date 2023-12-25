using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ImageBlinker : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Image imageRenderer;    

    public float speed = 0.5f;
    private void Start() 
    {
        if(sprRenderer != null) StartCoroutine(Blink(sprRenderer, speed));
        if(imageRenderer != null) StartCoroutine(Blink(imageRenderer, speed));
    }

    public void StartBlinkingEffect(SpriteRenderer spriteRenderer, float blinkSpeed)
    {
        StartCoroutine(Blink(sprRenderer, speed));
    }
    public void StartBlinkingEffect(Image spriteRenderer, float blinkSpeed)
    {
        StartCoroutine(Blink(sprRenderer, speed));
    }

    private IEnumerator Blink(SpriteRenderer spriteRenderer, float blinkSpeed)
    {
        while (true)
        {            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            yield return new WaitForSeconds(blinkSpeed);
            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
    private IEnumerator Blink(Image spriteRenderer, float blinkSpeed)
    {
        while (true)
        {            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            yield return new WaitForSeconds(blinkSpeed);
            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
