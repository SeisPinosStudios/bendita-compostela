using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Damage : BasicCardEffect
{
    public static event Action<GameObject, GameObject, CardData> OnAttack = delegate { };
    public static void Effect(string damage, CardData card, GameObject user, GameObject target)
    {
        OnAttack(target, user, card);

        var cardUser = user.GetComponent<Entity>();

        var entityEffectsManager = user.GetComponent<EntityEffectsManager>();
        var frenzyStacks = entityEffectsManager.frenzyAttacks.ContainsKey(card) ? entityEffectsManager.frenzyAttacks[card] : 0;

        Marked(damage, card, target);

        user.GetComponent<EntityDisplay>().AttackAnimation();
        target.GetComponent<Entity>().SufferDamage(int.Parse(damage), cardUser.attackBonus + frenzyStacks, cardUser.ComputeAttackMultiplier(), false);

        OnAttack(target, user, card);
        Frenzy(card, cardUser);
    }

    private static void Marked(string damage, CardData card, GameObject target)
    {
        if (BattleManager.Instance.enemies.Count <= 1) return;
        if (target.GetComponent<Entity>().entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Marked))
        {
            var enemies = BattleManager.Instance.enemies.Where(enemy => enemy.gameObject != target).ToList();
            enemies[UnityEngine.Random.Range(0, enemies.Count)].SufferDamage(Mathf.CeilToInt(card.GetDamage() * 0.5f), 0, 0, true);
            target.GetComponent<Entity>().entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Marked, 1);
        }
    }

    public static void Frenzy(CardData card, Entity user)
    {
        if (!user.entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Frenzy)) return;
        if (user.entityEffectsManager.frenzyAttacks.ContainsKey(card)) user.entityEffectsManager.frenzyAttacks[card] += 1;
        else user.entityEffectsManager.frenzyAttacks.Add(card, 1);

        user.entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Frenzy, 1);
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
        finalDamage = Mathf.RoundToInt((finalDamage + userDamageBonus - targetDefenseBonus) * userDamageMultiplier);
        finalDamage += Mathf.RoundToInt((1 - targetDefenseMultiplier) * finalDamage);
        finalDamage = Mathf.RoundToInt(Mathf.Clamp(finalDamage, 0, float.PositiveInfinity));
        return $"Realiza {finalDamage} puntos de daño.";
    }
}
