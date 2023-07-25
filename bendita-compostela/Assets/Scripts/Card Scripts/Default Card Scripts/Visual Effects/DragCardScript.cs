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
    public static event Action onUsing, onReturning;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        index = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.y < 400)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root.gameObject.transform as RectTransform, 
                                                                    eventData.position, transform.root.GetComponent<Canvas>().worldCamera, out pos);
            transform.position = transform.root.gameObject.transform.TransformPoint(pos);
            onReturning();
        }

        if (eventData.position.y > 400)
        {
            transform.position = new Vector3(1920 / 2, 20.0f, 0.0f);
            onUsing();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastResult hit = RaycastUtils.Raycast("Enemy");
        if (eventData.position.y < 400 || !hit.isValid) { ReturnCardToHand(); return; }
        onReturning();
        UseCardOnTarget(hit.gameObject);
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
        gameObject.GetComponent<Card>().useCard(target);
        Destroy(this);
    }
}
