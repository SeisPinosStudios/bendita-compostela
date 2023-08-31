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
    public static event Action onUsing, onReturning;

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
            transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + 100, 0.0f);
            onUsing();
        }
        else
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform, 
                                                                    eventData.position, transform.root.GetComponent<Canvas>().worldCamera, out pos);
            transform.position = transform.root.gameObject.transform.TransformPoint(pos);
            onReturning();
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        cardDisplay.dragging = false;
        if(!cardData.printArrow && eventData.position.y > 400) { UseCard(); return; }

        var hit = RaycastUtils.Raycast2D("Enemy");
        if (eventData.position.y < 400 || !hit) { ReturnCardToHand(); return; }
        UseCardOnTarget(hit);
    }
    private void ReturnCardToHand()
    {
        transform.SetParent(parent);
        transform.SetSiblingIndex(index);
        onReturning();
    }
    private void UseCardOnTarget(GameObject target)
    {
        print($"Card used on {target.name}");
        onReturning();
        card.UseCard(target);
    }
    private void UseCard()
    {
        print($"Card used");
        onReturning();
        card.UseCard();
    }
}
