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
    GameObject lastHittedEnemy = null;

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
        
        if (eventData.position.y > 300 && cardData.printArrow)
        {
            GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 120.0f, 1.0f));
            var hit = RaycastUtils.Raycast2D("Enemy");
            
            if (hit)
            {
                hit.GetComponent<EntityDisplay>().HighlightOn();
                lastHittedEnemy = hit;
            }
            else 
            {
                lastHittedEnemy?.GetComponent<EntityDisplay>().HighlightOff();
            }
            
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
        cardDisplay.dragging = false;
        if(!cardData.printArrow && eventData.position.y > 400) { UseCard(eventData); return; }

        var hit = RaycastUtils.Raycast2D("Enemy");
        hit?.GetComponent<EntityDisplay>().HighlightOff();
        if (eventData.position.y < 400 || !hit) { ReturnCardToHand(eventData); return; }
        UseCardOnTarget(eventData, hit);

    }
    private void ReturnCardToHand(PointerEventData eventData)
    {
        cardInspection.OnPointerExit(eventData);
        OnReturning();
    }
    private void UseCardOnTarget(PointerEventData eventData, GameObject target)
    {
        cardInspection.OnPointerExit(eventData);
        print($"Card used on {target.name}");
        OnReturning();
        card.UseCard(target);
    }
    private void UseCard(PointerEventData eventData)
    {
        cardInspection.OnPointerExit(eventData);
        print($"Card used");
        OnReturning();
        card.UseCard();
    }
}
