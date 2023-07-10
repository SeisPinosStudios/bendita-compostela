using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData cardData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopSelectionManager.Instance.ShowCard(cardData, transform.GetSiblingIndex());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopSelectionManager.Instance.OnMouseExitClearCard();
    }
}
