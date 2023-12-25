using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HammerCard : IBasicCardEffect
{
    public static event Action<CardData, GameObject> OnHammerCard = delegate { };

    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        OnHammerCard(card, target);
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return null;
    }
}
