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
    [SerializeField] AudioClip drawCardSoundEffect;

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
        while (!WeaponOnTop(deck))
        {
            deck = ListUtils.Shuffle(BattleManager.Instance.player.playerData.deck);
        }
        foreach(CardData card in deck) deckQueue.Enqueue(card);
    }

    #region Basic Methods
    public void DrawCard(int amount)
    {
        StartCoroutine(DrawCardCoroutine(amount));
    }
    public IEnumerator DrawCardCoroutine(int amount)
    {
        var delay = 1.0f / cardsToDraw;
        yield return new WaitUntil(() => deckQueue != null);
        for (int i = 0; i < amount; i++)
        {
            if (deckQueue.Count <= 0) break;
            card.cardData = deckQueue.Dequeue();
            Instantiate(card, hand);
            SoundManager.Instance.PlaySound(drawCardSoundEffect);
            yield return new WaitForSeconds(delay);
        }
        Debug.Log($"Draw Coroutine Finished");
        yield return null;
    }
    public void ReturnCards()
    {
        StartCoroutine(ReturnCardsCoroutine());
    }
    public IEnumerator ReturnCardsCoroutine()
    {
        var delay = 1.0f / hand.childCount;
        while(hand.childCount > 0)
        {
            var card = hand.GetChild(0).GetComponent<CardDataContainer>();
            if(card.cardData is not WeaponAttackData) deckQueue.Enqueue(card.cardData);
            Destroy(card.gameObject);
            SoundManager.Instance.PlaySound(drawCardSoundEffect);
            yield return new WaitForSeconds(delay);
        }
        yield return null;
    }
    public void AddCardToDeck(CardData card)
    {
        deckQueue.Enqueue(card);
    }
    public bool WeaponOnTop(List<CardData> deck)
    {
        for (int i = 0; i < 5; i++) if (deck[i] is WeaponData) return true;
        return false;
    }
    #endregion

    public void SetCardsToDraw(int amount)
    {
        cardsToDraw = amount;
    }
}
