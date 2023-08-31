using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BaseWeapon
{
    
    private void Awake()
    {
        weaponId = 3;
        Damage.OnAttack += Style;
        Damage.OnAttack2 += ChestSynergy;

        player = BattleManager.Instance.player;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();

        if (legSynergy) LegSynergy();
    }

    private void Style(GameObject target, CardData card)
    {
        if (target.GetComponent<Entity>().GetType() == typeof(Player)) return;
        
        var index = BattleManager.Instance.enemies.IndexOf(target.GetComponent<Enemy>());
        if (index == BattleManager.Instance.enemies.Count - 1) return;
        
        BattleManager.Instance.enemies[index+1]
            .SufferDamage(Mathf.RoundToInt(card.GetDamage() * GetStyleMultiplier()), player.damageBonus, player.damageMultiplier, false);
    }

    private float GetStyleMultiplier()
    {
        switch (GetStyleLevel())
        {
            case 0:
                return 0.5f;
            case 1:
                return 1.0f;
            case 2:
                return 2.0f;
        }

        return 0.0f;
    }

    private void ChestSynergy(GameObject target, GameObject user, CardData card)
    {
        if(!chestSynergy) return;
        if(user.GetComponent<Entity>().GetType() == typeof(Player)) return;
        if (!user.GetComponent<EntityEffectsManager>().Suffering(TAlteredEffects.AlteredEffects.Invulnerable)) return;

        target.GetComponent<Entity>().SufferDamage(Mathf.RoundToInt(card.GetDamage() * GetStyleMultiplier()), 0, 0.0f, true);
    }

    private void LegSynergy()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.GuardedMultiplier(GetLegLevel() < 2 ? -0.25f : -0.5f);

        if (GetLegLevel() != 0) player.entityEffectsManager.GuardedMultiplier(0.25f);
    }
}
