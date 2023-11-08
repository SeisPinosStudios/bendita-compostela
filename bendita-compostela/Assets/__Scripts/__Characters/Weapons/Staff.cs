using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : BaseWeapon
{
    int healingBonus;
    private new void Awake()
    {
        base.Awake();

        weaponId = 4;

        EnableStyle();

        AddListeners();

        if (chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();
    }

    private void OnDestroy()
    {
        RemoveListeners();

        DisableStyle();

        if (chestSynergy) DisableChestSynergy();
        if (legSynergy) DisableLegSynergy();
    }

    #region Style
    private void EnableStyle() {
        healingBonus = BattleManager.Instance.enemies.Count - 1 + styleLevel;
        player.HealingBonus(healingBonus);
    }
    private void DisableStyle() {
        healingBonus = BattleManager.Instance.enemies.Count - 1 + styleLevel;
        player.HealingBonus(-healingBonus);
    }
    private void UpdateStyle() {
        player.HealingBonus(-healingBonus);
        healingBonus = BattleManager.Instance.enemies.Count - 1 + styleLevel;
        player.HealingBonus(healingBonus);
    }
    #endregion

    #region Synergies
    private void EnableChestSynergy() {
        AttackDeckManager.Instance.ReduceAttackCost(GetChestLevel() + 1);
    }
    private void EnableLegSynergy() {
        player.HealingBonus(GetLegLevel() + 1);
    }

    private void DisableChestSynergy() {
        AttackDeckManager.Instance.ReduceAttackCost(-(GetChestLevel() + 1));
    }
    private void DisableLegSynergy() {
        player.HealingBonus(-(GetLegLevel() + 1));
    }
    #endregion

    #region Listeners
    private void AddListeners() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.OnDeath += UpdateStyle;
    }
    private void RemoveListeners() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.OnDeath -= UpdateStyle;
    }
    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con cetro: reduce el coste de los ataques del cetro en {GameManager.Instance.playerData.chestArmor.synergyLevel+1} puntos de energía.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con cetro: Tus curaciones aumentan su efecto en {1+GameManager.Instance.playerData.legArmor.synergyLevel} puntos de vida.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: el límite para que los enemigos sufran el doble de daño por Quemado se reduce a {6-(1+weapon.styleLevel)} " +
            $"cargas de Quemado.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: el límite para que los enemigos sufran el doble de daño por Quemado se reduce a {6 - (1 + styleLevel)} " +
            $"cargas de Quemado.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con cetro: reduce el coste de los ataques del cetro en {synergyLevel + 1} puntos de energía.";

        else
            return $"Sinergia con cetro: Tus curaciones aumentan su efecto en {1 + synergyLevel} puntos de vida.";
    }
    #endregion
}
