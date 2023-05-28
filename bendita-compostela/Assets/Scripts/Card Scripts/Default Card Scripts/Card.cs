using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header ("Card Info")]
    public CardData cardData;

    private void Awake()
    {

    }
    public void useCard()
    {
        StartCoroutine(useCardCorroutine());
    }

    IEnumerator useCardCorroutine()
    {
        for (int i = 0; i < cardData.cardEffects.Count; i++)
        {
            Type.GetType(cardData.cardEffects[i].ToString() + ", CardEffects").GetMethod("effect").Invoke(null, new object[] { cardData.cardEffectsValues[i] });
            yield return new WaitForSeconds(1.0f);
        }

        yield return null;
    }
}
