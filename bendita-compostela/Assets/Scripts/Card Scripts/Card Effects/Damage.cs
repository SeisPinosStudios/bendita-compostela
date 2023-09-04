using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damage : BasicCardEffect
{
    public static event Action<GameObject, CardData> OnAttack = delegate { };
    public static event Action<GameObject, GameObject, CardData> OnAttack2 = delegate { };
    public static void Effect(string damage, CardData card, GameObject user, GameObject target)
    {
        OnAttack(target, card);
        OnAttack2(target, user, card);

        var cardUser = user.GetComponent<Entity>();

        var entityEffectsManager = user.GetComponent<EntityEffectsManager>();
        var frenzyStacks = entityEffectsManager.frenzyAttacks.ContainsKey(card) ? entityEffectsManager.frenzyAttacks[card] : 0;


        user.GetComponent<EntityDisplay>().AttackAnimation();
        target.GetComponent<Entity>().SufferDamage(int.Parse(damage), cardUser.attackBonus + frenzyStacks, cardUser.ComputeAttackMultiplier(), false);

    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        var finalDamage = card.GetDamage();
        var frenzyStacks = user && user.entityEffectsManager.frenzyAttacks.ContainsKey(card) ? user.entityEffectsManager.frenzyAttacks[card] : 0;

        var userDamageBonus = user ? user.attackBonus : 0;
        var targetDefenseBonus = target ? target.defenseBonus : 0;
        var userDamageMultiplier = user ? user.GetAttackMultiplier() : 1;
        var targetDefenseMultiplier = target ? target.GetAttackMultiplier() : 1;

        finalDamage += frenzyStacks;
        finalDamage = Mathf.RoundToInt(Mathf.Clamp((finalDamage + userDamageBonus - targetDefenseBonus) 
                       * userDamageMultiplier / targetDefenseMultiplier, 0, 99));
        return $"Realiza {finalDamage} puntos de daño.";
    }
}
