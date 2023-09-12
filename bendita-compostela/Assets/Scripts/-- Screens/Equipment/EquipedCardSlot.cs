using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipedCardSlot : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public CardDataContainer cardDataCont { get; private set; }
    [field: SerializeField] public Image miniArt { get; private set; }
    [field: SerializeField] public int amount { get; private set; } = 1;
    [field: SerializeField] public TextMeshProUGUI amountText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI cardName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI cardCost { get; private set; }

    private void Awake()
    {
        miniArt.sprite = cardDataCont.cardData.miniArt;
        cardName.text = cardDataCont.cardData.cardName;
        cardCost.text = cardDataCont.cardData.cost.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.playerData.inventory.Add(cardDataCont.cardData);
        GameManager.Instance.playerData.deck.Remove(cardDataCont.cardData);

        if (amount <= 1) Destroy(gameObject);
        else amount--;

        DeckBuilderManager.Instance.UpdateShownCards();
    }
}
