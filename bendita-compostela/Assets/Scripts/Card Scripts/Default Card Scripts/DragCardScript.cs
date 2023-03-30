using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initialPosition;
    private Transform parent;
    private int index;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.position;
        parent = transform.parent;
        index = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);
        if (Vector3.Distance(transform.position, initialPosition) > 100f)
        {
            // card was used
            Debug.Log("Card used!");
        }
        else
        {
            // card returned to hand
            transform.position = initialPosition;
            transform.parent = parent;
            transform.SetSiblingIndex(index);
        }
    }
}
