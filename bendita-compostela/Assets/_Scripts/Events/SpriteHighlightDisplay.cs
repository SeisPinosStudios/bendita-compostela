using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SpriteHighlightDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer highlight;
    [SerializeField] Sound hoverSound;    
    private void OnMouseEnter() {
        if(hoverSound.AudioClip != null) SoundManager.Instance.PlaySound(hoverSound.AudioClip,hoverSound.Volume);
        highlight.enabled = true;
    }
    private void OnMouseExit() {
        highlight.enabled = false;
    }
}
