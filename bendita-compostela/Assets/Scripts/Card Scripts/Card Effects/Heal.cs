using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heal : BasicCardEffect
{
    public static event Action<CardData, GameObject> OnHeal;
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var entity = user.GetComponent<Entity>();
        entity.RestoreHealth(int.Parse(data), entity.healingBonus, entity.healingMultiplier);
        OnHeal(card, user);
    }
}
