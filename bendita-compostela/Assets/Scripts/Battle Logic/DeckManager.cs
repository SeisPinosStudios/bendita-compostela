using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [field:SerializeField] public static DeckManager Instance { get; private set; }
    [SerializeField] Queue<CardData> deckQueue = new Queue<CardData>();
    [SerializeField] Transform hand;
    [SerializeField] CardDataContainer card;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        StartCoroutine(SetupDeck());
    }
    private IEnumerator SetupDeck()
    {
        yield return new WaitUntil(() => BattleManager.Instance != null);
        var deck = Shuffle(((PlayerData)BattleManager.Instance.player.entityDataContainer.entityData).deck);
        foreach(CardData card in deck) deckQueue.Enqueue(card);
    }

    public void DrawCard(int amount)
    {
        StartCoroutine(DrawCardCoroutine(amount));
    }
    private IEnumerator DrawCardCoroutine(int amount)
    {
        yield return new WaitUntil(() => deckQueue != null);
        for(int i = 0; i < amount; i++)
        {
            card.cardData = deckQueue.Dequeue();
            Instantiate(card, hand);
            yield return new WaitForSeconds(0.4f);
        }
    }

    #region Basic Methods
    private List<CardData> Shuffle(List<CardData> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var randomPos = Random.Range(0, deck.Count);
            var temporalValue = deck[i];
            deck[i] = deck[randomPos];
            deck[randomPos] = temporalValue;
        }

        return deck;
    }
    #endregion
}
