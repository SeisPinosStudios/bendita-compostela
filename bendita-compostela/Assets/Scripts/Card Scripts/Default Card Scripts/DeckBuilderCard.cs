using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilderCard : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public CardDataContainer cardDataCont { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        var card = GameManager.Instance.playerData.inventory.Find(card => card.cardName == cardDataCont.cardData.cardName);

        GameManager.Instance.playerData.deck.Add(card);
        GameManager.Instance.playerData.inventory.Remove(card);
        DeckBuilderManager.Instance.displayedCards.Remove(card);
        if (!DeckBuilderManager.Instance.displayedCards.Find(card => card.cardName == cardDataCont.cardData.cardName)) Destroy(gameObject);
        DeckBuilderManager.Instance.UpdateDeckCards();
    }
}
