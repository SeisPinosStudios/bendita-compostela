using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [field:SerializeField] public static DeckManager Instance { get; private set; }
    [SerializeField] Queue<CardData> deckQueue = new Queue<CardData>();
    [SerializeField] Transform hand;
    [SerializeField] CardDataContainer card;

    [SerializeField] float delaySeconds;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        StartCoroutine(SetupDeck());
    }
    private IEnumerator SetupDeck()
    {
        yield return new WaitUntil(() => BattleManager.Instance != null);
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
        yield return new WaitUntil(() => deckQueue != null);
        for (int i = 0; i < amount; i++)
        {
            card.cardData = deckQueue.Dequeue();
            Instantiate(card, hand);
            yield return new WaitForSeconds(delaySeconds);
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
}
