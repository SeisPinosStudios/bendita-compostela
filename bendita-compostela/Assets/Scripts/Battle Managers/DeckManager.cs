using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [field:SerializeField] public static DeckManager Instance { get; private set; }
    [field: SerializeField] public int cardsToDraw { get; private set; } = 5;
    [SerializeField] Queue<CardData> deckQueue = new Queue<CardData>();
    [SerializeField] Transform hand;
    [SerializeField] CardDataContainer card;

    [SerializeField] float delaySeconds;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SetupDeck());
    }
    private IEnumerator SetupDeck()
    {
        yield return new WaitForSeconds(0.1f);
        var deck = ListUtils.Shuffle(BattleManager.Instance.player.playerData.deck);
        foreach(CardData card in deck) deckQueue.Enqueue(card);
    }

    #region Basic Methods
    public void DrawCard(int amount)
    {
        StartCoroutine(DrawCardCoroutine(amount));
    }
    public IEnumerator DrawCardCoroutine(int amount)
    {
        yield return new WaitUntil(() => deckQueue != null && deckQueue.Count > 0);
        for (int i = 0; i < amount; i++)
        {
            card.cardData = deckQueue.Dequeue();
            Instantiate(card, hand);
            yield return new WaitForSeconds(delaySeconds);
            if (deckQueue.Count <= 0) break;
        }
    }
    public void ReturnCards()
    {
        StartCoroutine(ReturnCardsCoroutine());
    }
    public IEnumerator ReturnCardsCoroutine()
    {
        while(hand.childCount > 0)
        {
            var card = hand.GetChild(0);
            deckQueue.Enqueue(card.GetComponent<CardDataContainer>().cardData);
            Destroy(card.gameObject);
            yield return new WaitForSeconds(delaySeconds);
        }
    }
    public void AddCardToDeck(CardData card)
    {
        deckQueue.Enqueue(card);
    }
    #endregion

    public void SetCardsToDraw(int amount)
    {
        cardsToDraw = amount;
    }
}
