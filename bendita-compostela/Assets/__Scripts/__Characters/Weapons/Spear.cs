using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BaseWeapon
{
    private new void Awake() {
        base.Awake();

        weaponId = 3;
        
        AddListeners();

        if(chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();
    }

    private void OnDestroy() {
        RemoveListeners();

        if(chestSynergy) DisableChestSynergy();
        if (legSynergy) DisableLegSynergy();
    }

    #region Style
    private void Style(GameObject target, GameObject user, CardData card) {
        if (user.GetComponent<Entity>().GetType() != typeof(Player)) return;

        var enemy = target.GetComponent<Enemy>();
        var enemyIndex = BattleManager.Instance.enemies.IndexOf(enemy);

        if (enemyIndex == BattleManager.Instance.enemies.Count -1) return;

        var cardDamage = card.GetDamage() + (-3 + 2 * styleLevel);

        BattleManager.Instance.enemies[enemyIndex - 1].SufferDamage(cardDamage, player.attackBonus, player.GetAttackMultiplier(), false);
    }
    #endregion

    #region Synergies
    private void EnableChestSynergy() {
        Damage.OnAttack += ChestSynergy;
    }
    private void EnableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.GuardedMultiplier(GetLegLevel() < 2 ? -0.25f : -0.5f);
        if (GetLegLevel() != 0) player.entityEffectsManager.GuardedMultiplier(0.25f);
    }

    private void DisableChestSynergy() {
        Damage.OnAttack -= ChestSynergy;
    }
    private void DisableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.GuardedMultiplier(GetLegLevel() < 2 ? 0.25f : 0.5f);
        if (GetLegLevel() != 0) player.entityEffectsManager.GuardedMultiplier(-0.25f);
    }

    private void ChestSynergy(GameObject target, GameObject user, CardData card) {
        if (user.GetComponent<Entity>().GetType() == typeof(Player)) return;
        if (!target.GetComponent<EntityEffectsManager>().Suffering(TAlteredEffects.AlteredEffects.Invulnerable)) return;

        user.GetComponent<Entity>().SufferDamage(Mathf.RoundToInt(card.GetDamage() * (0.5f + (0.25f * GetChestLevel()))), 0, 0.0f, true);
    }
    #endregion

    #region Listeners
    private void AddListeners() {
        Damage.OnAttack += Style;
    }
    private void RemoveListeners() {
        Damage.OnAttack -= Style;
    }
    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con lanza: cuando recibas un ataque teniendo el efecto Invulnerable, devuelve un" +
            $" {0.5f + (0.25f * GameManager.Instance.playerData.chestArmor.synergyLevel) * 100}% del daño.";
    }
    public static string GetLegDescription()
    {
        if (GameManager.Instance.playerData.legArmor.synergyLevel != 0)
            return $"Sinergia con lanza: el efecto En Guardia de los enemigos pasa a bloquear " +
                $"{0.5 - (GameManager.Instance.playerData.legArmor.synergyLevel < 2 ? 0.25f : 0.5f)} del daño. Tu efecto En Guardia pasa a bloquear 75% del da�o.";

        return $"Sinergia con lanza: el efecto En Guardia de los enemigos pasa a bloquear 25% del daño.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: cuando atacas a un enemigo, el enemigo posicionado detrás sufrirá un {GetStyleMultiplier(weapon.styleLevel)*100}% " +
            $"del daño.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: cuando atacas a un enemigo, el enemigo posicionado detrás sufrirá un {GetStyleMultiplier(styleLevel) * 100}% " +
            $"del daño.";
    }
    private static float GetStyleMultiplier(int styleLevel)
    {
        switch (styleLevel)
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
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con lanza: cuando recibas un ataque teniendo el efecto Invulnerable, devuelve un" +
            $" {0.5f + (0.25f * synergyLevel) * 100}% del daño.";

        else
            if (synergyLevel != 0)
                return $"Sinergia con lanza: el efecto En Guardia de los enemigos pasa a bloquear " +
                    $"{0.5 - (synergyLevel < 2 ? 0.25f : 0.5f)} del daño. Tu efecto En Guardia pasa a bloquear 75% del daño.";

            return $"Sinergia con lanza: el efecto En Guardia de los enemigos pasa a bloquear 25% del daño.";
    }
    #endregion
}
