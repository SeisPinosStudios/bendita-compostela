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
        print($"Used card {cardData.cardName}");
        for (int i = 0; i < cardData.cardEffects.Count; i++)
        {
            Type.GetType(cardData.cardEffects[i].ToString())
                .GetMethod("Effect").Invoke(null, new object[] { cardData.cardEffectsValues[i], cardData, TurnManager.Instance.entityTurn.gameObject, target });
            yield return new WaitForSeconds(1.0f);
        }

        yield return null;
    }
}
