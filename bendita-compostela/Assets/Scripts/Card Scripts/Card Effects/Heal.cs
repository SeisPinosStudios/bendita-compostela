using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heal : BasicCardEffect
{
    public static event Action<CardData, GameObject> OnHeal = delegate {};
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var entity = user.GetComponent<Entity>();
        entity.RestoreHealth(int.Parse(data), entity.healingBonus, entity.healingMultiplier);
        entity.GetComponent<EntityDisplay>().Heal();
        OnHeal(card, user);
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        var data = card.GetEffect(CardData.Effect.Heal);

        var userHealingBonus = user ? user.healingBonus : 0;
        var userHealingMultiplier = user ? user.healingMultiplier : 1;

        var finalHeal = Mathf.RoundToInt((int.Parse(data[0]) + userHealingBonus) * userHealingMultiplier);
        return $"Cura {finalHeal}.";
    }
}
