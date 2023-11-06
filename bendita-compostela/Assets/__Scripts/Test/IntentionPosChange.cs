using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntentionPosChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float parentSpriteHeight;
    public Vector3 screenPosition;
    public bool autoAdjust = false;
    public Camera camara;
    private void Start()
    {
        if (autoAdjust) SetUpPosition(); 
    }
    public void SetUpPosition()
    {
        parentSpriteHeight = spriteRenderer.bounds.size.y;
        screenPosition = camara.WorldToScreenPoint(new Vector3(0f, parentSpriteHeight, 0f));
        transform.localPosition = new Vector3(transform.localPosition.x, screenPosition.y, transform.localPosition.z);
    }
}
