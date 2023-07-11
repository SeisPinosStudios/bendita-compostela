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

    public void useCard(GameObject target)
    {
        StartCoroutine(useCardCorroutine(target));
    }

    IEnumerator useCardCorroutine(GameObject target)
    {
        for (int i = 0; i < cardData.cardEffects.Count; i++)
        {
            Type.GetType(cardData.cardEffects[i].ToString() + ", CardEffects").GetMethod("effect").Invoke(null, new object[] { cardData.cardEffectsValues[i], target });
            yield return new WaitForSeconds(1.0f);
        }

        yield return null;
    }
}
