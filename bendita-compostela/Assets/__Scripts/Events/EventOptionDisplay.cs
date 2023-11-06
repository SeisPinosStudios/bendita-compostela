using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventOptionDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image highlight;
    [SerializeField] Sound hoverSound;
    [SerializeField] bool active = true;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!active) return;
        if(hoverSound.AudioClip != null) SoundManager.Instance.PlaySound(hoverSound.AudioClip,hoverSound.Volume);
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }
}
