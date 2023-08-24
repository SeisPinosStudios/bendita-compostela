using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header ("Card Info")]
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
    }

    public void UseCard(GameObject target)
    {
        StartCoroutine(UseCardCorroutine(target));
    }

    public void UseCard()
    {
        StartCoroutine(UseCardCorroutine(null));
    }

    IEnumerator UseCardCorroutine(GameObject target)
    {
        var player = BattleManager.Instance.player;

        if (!player.ConsumeEnergy(GetEnergyCost(player))) yield break;

        print($"Used card {cardData.cardName}");
        for (int i = 0; i < cardData.cardEffects.Count; i++)
        {
            Type.GetType(cardData.cardEffects[i].ToString())
                .GetMethod("Effect").Invoke(null, new object[] { cardData.cardEffectsValues[i], cardData, TurnManager.Instance.entityTurn.gameObject, target });
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(this.gameObject);
        yield return null;
    }

    private int GetEnergyCost(Player player)
    {
        var energyCost = cardData.cost;
        var playerEffectsManager = player.GetComponent<EntityEffectsManager>();

        if (playerEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Exhaust))
        {
            energyCost += 1;
            playerEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Exhaust, 1);
        }

        return energyCost;
    }
}
