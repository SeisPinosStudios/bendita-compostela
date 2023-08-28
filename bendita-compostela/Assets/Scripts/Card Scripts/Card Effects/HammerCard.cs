using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HammerCard : BasicCardEffect
{
    public static event Action<CardData, GameObject> OnHammerCard = delegate { };

    public static void Effec(string data, CardData card, GameObject user, GameObject target)
    {
        OnHammerCard(card, target);
    }
}
