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
        target.GetComponent<EntityDisplay>().Heal();
        OnHeal(card, user);
    }
}
