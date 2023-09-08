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
        return $"Sinergia con cetro: reduce el coste de los ataques del cetro en {GameManager.Instance.playerData.chestArmor.synergyLevel+1} puntos de energ�a.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con cetro: Tus curaciones aumentan su efecto en {1+GameManager.Instance.playerData.legArmor.synergyLevel} puntos de vida.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: el l�mite para que los enemigos sufran el doble de da�o por Quemado se reduce a {6-(1+weapon.styleLevel)} " +
            $"cargas de Quemado.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: el l�mite para que los enemigos sufran el doble de da�o por Quemado se reduce a {6 - (1 + styleLevel)} " +
            $"cargas de Quemado.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con cetro: reduce el coste de los ataques del cetro en {synergyLevel + 1} puntos de energ�a.";

        else
            return $"Sinergia con cetro: Tus curaciones aumentan su efecto en {1 + synergyLevel} puntos de vida.";
    }
    #endregion
}
