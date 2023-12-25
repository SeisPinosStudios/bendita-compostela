using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : BaseWeapon
{
    private new void Awake()
    {
        base.Awake();

        weaponId = 1;

        AddListeners();

        if (chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();
    }
    private void OnDestroy()
    {
        RemoveListeners();

        if (chestSynergy) DisableChestSynergy();
        if (legSynergy) DisableLegSynergy();
    }

    #region Style
    private void Style(CardData card, GameObject target) {
        var enemies = BattleManager.Instance.enemies;
        enemies.Remove(target.GetComponent<Enemy>());

        var cardDamage = card.GetDamage() + (-3 + 2 * styleLevel);

        foreach (Enemy enemy in enemies) enemy.SufferDamage(cardDamage, player.attackBonus, player.GetAttackMultiplier(), false);
    }
    #endregion

    #region Synergies
    private void EnableChestSynergy() {
        player.GetComponent<EntityEffectsManager>().VulnerableMultiplier(GetPlayerMultiplier());
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.GetComponent<EntityEffectsManager>().VulnerableMultiplier(GetEnemyMultiplier());
    }
    private void DisableChestSynergy() {
        player.GetComponent<EntityEffectsManager>().VulnerableMultiplier(-GetPlayerMultiplier());
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.GetComponent<EntityEffectsManager>().VulnerableMultiplier(-GetEnemyMultiplier());
    }

    private void EnableLegSynergy() {
        player.AttackBonus(GetDamageBonus());
    }
    private void DisableLegSynergy() {
        player.AttackBonus(-GetDamageBonus());
    }

    private float GetEnemyMultiplier() {
        switch (chestLevel) {
            case 0:
                return 0.25f;
            case 1:
                return 0.5f;
            case 2:
                return 1.0f;
        }

        return 0.0f;
    }
    private float GetPlayerMultiplier() {
        switch (chestLevel) {
            case 0:
                return 0.25f;
            case 1:
                return 0.5f;
            case 2:
                return 0.5f;
        }

        return 0.0f;
    }
    private int GetDamageBonus() {
        return legLevel == 0 ? 1 : legLevel * 2;
    }
    #endregion

    #region Listeners
    public void AddListeners() {
        HammerCard.OnHammerCard += Style;
    }
    public void RemoveListeners() {
        HammerCard.OnHammerCard -= Style;
    }
    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con martillo: aumenta el daño contra objetivos vulnerables a un {GetEnemyMultiplier(GameManager.Instance.playerData.chestArmor.synergyLevel)}%. " +
            $"También aumenta el daño extra que sufres estando vulnerable a un {GetPlayerMultiplier(GameManager.Instance.playerData.chestArmor.synergyLevel)}%.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con martillo: aumenta tu daño con ataques en {GetDamageBonus(GameManager.Instance.playerData.legArmor.synergyLevel)} puntos de daño.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: Los ataques del martillo hacen {-3 + 2 * weapon.styleLevel} de daño en área al resto de enemigos.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: Los ataques del martillo hacen {-3 + 2 * styleLevel} de daño en área al resto de enemigos.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con martillo: aumenta el daño contra objetivos vulnerables a un {GetEnemyMultiplier(synergyLevel)}%. " +
            $"También aumenta el daño extra que sufres estando vulnerable a un {GetPlayerMultiplier(synergyLevel)}%.";

        else
            return $"Sinergia con martillo: aumenta tu daño con ataques en {GetDamageBonus(synergyLevel)} puntos de daño.";
    }
    private static float GetEnemyMultiplier(int chestLevel)
    {
        switch (chestLevel)
        {
            case 0:
                return 0.25f;
            case 1:
                return 0.5f;
            case 2:
                return 1.0f;
        }

        return 0.0f;
    }
    private static float GetPlayerMultiplier(int chestLevel)
    {
        switch (chestLevel)
        {
            case 0:
                return 0.25f;
            case 1:
                return 0.5f;
            case 2:
                return 0.5f;
        }

        return 0.0f;
    }
    private static int GetDamageBonus(int legLevel)
    {
        return legLevel == 0 ? 1 : legLevel * 2;
    }
    #endregion
}
