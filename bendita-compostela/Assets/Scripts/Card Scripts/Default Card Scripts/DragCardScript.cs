using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag System Variables")]
    [SerializeField] Vector3 initialPosition_;
    [SerializeField] Transform parent_;
    [SerializeField] int index_;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition_ = transform.position;
        parent_ = transform.parent;
        index_ = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent_);
        if (Vector3.Distance(transform.position, initialPosition_) > 100f)
        {
            // card was used
            gameObject.GetComponent<Card>().useCard();
            //Destroy(gameObject);
        }
        else
        {
            // card returned to hand
            transform.position = initialPosition_;
            transform.parent = parent_;
            transform.SetSiblingIndex(index_);
        }
    }
}
