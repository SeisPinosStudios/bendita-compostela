using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : BaseWeapon
{
    private void Awake()
    {
        weaponId = 4;
        player = BattleManager.Instance.player;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();

        Style();

        if (chestSynergy) ChestSynergy();
    }

    private void Style()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.SetBurnThreshold(6 - 1 + (GetStyleLevel()));
    }
    private void ChestSynergy()
    {
        AttackDeckManager.Instance.ReduceAttackCost(GetChestLevel()+1);
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.SetBurnThreshold(6);

        if (legSynergy) player.HealingBonus(-(1 + GetLegLevel()));
    }
}
