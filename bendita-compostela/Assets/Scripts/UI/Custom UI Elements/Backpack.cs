using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Backpack : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public Transform deckBuilder { get; private set; }
    [field: SerializeField] public Transform equipment { get; private set; }
    [field: SerializeField] public bool open { get; private set; } = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        deckBuilder.localPosition += new Vector3(open ? -200 : 200, 0, 0);
        equipment.localPosition += new Vector3(0, open ? -200 : 200, 0);
        open = !open;
    }
}
