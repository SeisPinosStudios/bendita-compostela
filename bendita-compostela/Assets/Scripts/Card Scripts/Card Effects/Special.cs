using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : BasicCardEffect
{
    public static void Effect(string index, CardData card, GameObject user, GameObject target)
    {
        if(index != "") GameManager.Instance.playerData.deck.Add(SODataBase.special[int.Parse(index)]);
        GameManager.Instance.playerData.deck.Remove(card);
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return null;
    }
}
