using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAttack : IBasicCardEffect
{
    public static void Effect(string amount, CardData card, GameObject user, GameObject target)
    {
        AttackDeckManager.Instance.DrawFreeAttack(int.Parse(amount));
    }
    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return $"Roba dos cartas del mazo de ataques.";
    }
}
