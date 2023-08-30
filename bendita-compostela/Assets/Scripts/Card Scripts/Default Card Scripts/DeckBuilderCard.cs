using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilderCard : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public CardDataContainer cardDataCont { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.playerData.deck.Add(cardDataCont.cardData);
        GameManager.Instance.playerData.inventory.Remove(cardDataCont.cardData);
        DeckBuilderManager.Instance.displayedCards.Remove(cardDataCont.cardData);
        if (!DeckBuilderManager.Instance.displayedCards.Find(card => card.cardName == cardDataCont.cardData.cardName)) Destroy(gameObject);
    }
}
