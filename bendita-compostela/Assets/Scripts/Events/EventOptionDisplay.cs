using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventOptionDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image highlight;
    public void OnPointerEnter(PointerEventData eventData)
    {        
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }
}
