using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IBasicCardEffect
{
    public static void Effect(string index, CardData card, GameObject user, GameObject target)
    {
        GameManager.Instance.playerData.deck.Find(special => special.cardName == card.cardName);
        if (index != "") GameManager.Instance.playerData.deck.Add(SODataBase.special[int.Parse(index)]);
        
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return null;
    }
}
