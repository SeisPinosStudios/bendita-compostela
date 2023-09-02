using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragCardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag System Variables")]
    [SerializeField] Transform parent;
    [SerializeField] int index;
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;
    [SerializeField] Card card;
    [field: SerializeField] public CardDisplay cardDisplay { get; private set; }
    [field: SerializeField] public CardInspection cardInspection { get; private set; }
    public static event Action OnUsing, OnReturning;

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        index = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
        cardDisplay.dragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        if (eventData.position.y > 400 && cardData.printArrow)
        {
            GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 120.0f, 1.0f));

            OnUsing();
        }
        else
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform, 
                                                                    eventData.position, transform.root.GetComponent<Canvas>().worldCamera, out pos);
            transform.position = transform.root.gameObject.transform.TransformPoint(pos);
            OnReturning();
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        cardInspection.OnPointerExit(eventData);
        
        cardDisplay.dragging = false;
        if(!cardData.printArrow && eventData.position.y > 400) { UseCard(); return; }

        var hit = RaycastUtils.Raycast2D("Enemy");
        if (eventData.position.y < 400 || !hit) { ReturnCardToHand(eventData); return; }
        UseCardOnTarget(hit);
    }
    private void ReturnCardToHand(PointerEventData eventData)
    {
        cardInspection.OnPointerExit(eventData);
        OnReturning();
    }
    private void UseCardOnTarget(GameObject target)
    {
        print($"Card used on {target.name}");
        OnReturning();
        card.UseCard(target);
    }
    private void UseCard()
    {
        print($"Card used");
        OnReturning();
        card.UseCard();
    }
}
