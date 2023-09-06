using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionPosChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float parentSpriteHeight;
        
    public void SetUpPosition()
    {
        parentSpriteHeight = spriteRenderer.bounds.size.y;
        transform.localPosition = new Vector3(transform.localPosition.x, parentSpriteHeight, transform.localPosition.z);
    }
}
