using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var entity = user.GetComponent<Entity>();
        entity.RestoreHealth(int.Parse(data), entity.healingBonus, entity.healingMultiplier);
    }
}
