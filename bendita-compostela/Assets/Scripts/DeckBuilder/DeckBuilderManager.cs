using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckBuilderManager : MonoBehaviour
{
    [field: SerializeField] public static DeckBuilderManager Instance { get; private set; }
    [field: SerializeField] public PlayerData playerData { get; private set; }
    [field: SerializeField] public CardDataContainer cardDisplayedPrefab { get; private set; }
    [field: SerializeField] public List<CardData> displayedCards { get; private set; }
    [field: SerializeField] public CardDataContainer cardsInDeckPrefab { get; private set; }
    [field: SerializeField] public Transform cardsDisplay { get; private set; }
    [field: SerializeField] public Transform deckDisplay { get; private set; }

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SetupCoroutine());
    }
    private IEnumerator SetupCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance && GameManager.Instance.playerData);
        playerData = GameManager.Instance.playerData;
        ShowCards();
        ShowDeckCards();
    }

    public void ShowCards()
    {
        var deck = playerData.inventory.Where(card => card is not ArmorData).OrderBy(card => card.cost).ThenBy(card => card.cardName);
        foreach(CardData card in deck)
        {
            cardDisplayedPrefab.cardData = card;
            if (!displayedCards.Find(displayedCard => displayedCard.cardName == card.cardName)) Instantiate(cardDisplayedPrefab, cardsDisplay);
            displayedCards.Add(card);
        }
    }
    public void ClearShownCards()
    {
        foreach (Transform child in cardsDisplay) Destroy(child.gameObject);
        displayedCards.Clear();
    }
    public void UpdateShownCards()
    {
        ClearShownCards();
        ShowCards();
    }
    public void ShowDeckCards()
    {
        var deck = playerData.deck.OrderBy(card => card.cost).ThenBy(card => card.cardName);
        foreach (CardData card in deck)
        {
            cardsInDeckPrefab.cardData = card;
            Instantiate(cardsInDeckPrefab, deckDisplay);
        }
    }
    public void ClearDeckCard()
    {
        foreach (Transform child in deckDisplay) Destroy(child.gameObject);
    }
    public void UpdateDeckCards()
    {
        ClearDeckCard();
        ShowDeckCards();
    }
}
