using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damage : BasicCardEffect
{
    public static event Action<GameObject, CardData> onAttack;
    public static void Effect(string damage, CardData card, GameObject user, GameObject target)
    {
        onAttack(target, card);
        Debug.Log("Damage card " + damage);
        target.GetComponent<Entity>().SufferDamage( int.Parse(damage), false);
    }
}
