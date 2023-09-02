using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInspection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public float verticalMovement { get; private set; }
    [field: SerializeField] public HorizontalLayoutGroup hand { get; private set; }
    [field: SerializeField] public int index { get; private set; }
    [field: SerializeField] public  static bool inspecting { get; private set; }

    private void Awake()
    {
        hand = transform.parent.GetComponent<HorizontalLayoutGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inspecting) return;

        inspecting = true;

        SetHandPanel(false);

        index = transform.GetSiblingIndex();
        transform.SetParent(transform.root, true);
        transform.localPosition += new Vector3(0.0f, verticalMovement, 0.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition -= new Vector3(0.0f, verticalMovement, 0.0f);
        transform.SetParent(hand.transform, true);
        transform.SetSiblingIndex(index);

        SetHandPanel(true);

        inspecting = false;
    }

    private void SetHandPanel(bool state)
    {
        hand.enabled = state;
    }

    private void OnDestroy()
    {
        SetHandPanel(true);
    }
}
