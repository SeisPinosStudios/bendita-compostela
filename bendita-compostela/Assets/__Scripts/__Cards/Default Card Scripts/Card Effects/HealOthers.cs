using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealOthers : BasicCardEffect
{
    public static event Action<CardData, GameObject> OnHealing;
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        OnHealing(card, target);
        var cardUser = user.GetComponent<Entity>();
        target.GetComponent<Entity>().RestoreHealth(int.Parse(data), cardUser.healingBonus, cardUser.healingMultiplier);
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        var data = card.GetEffect(CardData.Effect.HealOthers);
        var finalHeal = Mathf.RoundToInt((int.Parse(data[0]) + user.healingBonus) * user.healingMultiplier);
        return $"Cura {finalHeal} puntos de vida.";
    }
}
