using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageAll : BasicCardEffect
{
    public static event Action<GameObject, CardData> OnAttack = delegate { };
    public static event Action<GameObject, GameObject, CardData> OnAttack2 = delegate { };
    public static void Effect(string damage, CardData card, GameObject user, GameObject target)
    {
        OnAttack(target, card);
        OnAttack2(target, user, card);
        var entity = user.GetComponent<Entity>();
        var entityEffectsManager = user.GetComponent<EntityEffectsManager>();
        var frenzyStacks = entityEffectsManager.frenzyAttacks.ContainsKey(card) ? entityEffectsManager.frenzyAttacks[card] : 0;

        Debug.Log("Damage card " + damage);
        foreach(Enemy enemy in BattleManager.Instance.enemies)
            enemy.GetComponent<Entity>().SufferDamage(int.Parse(damage), entity.damageBonus + frenzyStacks, AttackMultiplier(entity, entityEffectsManager), false);
    }

    private static float AttackMultiplier(Entity entity, EntityEffectsManager entityEffectsManager)
    {
        var multiplier = entity.damageMultiplier;

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Lead))
        {
            multiplier -= 0.5f;
        }

        return multiplier;
    }
}
