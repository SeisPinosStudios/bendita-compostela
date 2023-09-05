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
        if (legSynergy) LegSynergy();
    }

    private void Style()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.SetBurnThreshold(6 - (1 + GetStyleLevel()));
    }
    private void ChestSynergy()
    {
        AttackDeckManager.Instance.ReduceAttackCost(GetChestLevel()+1);
    }
    private void LegSynergy()
    {
        BattleManager.Instance.player.HealingBonus(GetLegLevel() + 1);
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.SetBurnThreshold(6);

        if (legSynergy) player.HealingBonus(-(1 + GetLegLevel()));
    }

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con cetro: reduce el coste de los ataques del cetro en {GameManager.Instance.playerData.chestArmor.synergyLevel+1} puntos de energía.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con cetro: Tus curaciones aumentan su efecto en {1+GameManager.Instance.playerData.legArmor.synergyLevel} puntos de vida.";
    }
    public static string GetStyleDescription()
    {
        return $"Estilo: el límite para que los enemigos sufran el doble de daño por Quemado se reduce a {6-(1+BattleManager.Instance.player.weapon.styleLevel)} " +
            $"cargas de Quemado.";
    }
    #endregion
}
