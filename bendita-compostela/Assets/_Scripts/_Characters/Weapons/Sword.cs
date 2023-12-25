using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    int totalStyleAttacks = 1;
    int currentStyleAttacks;
    float styleMultiplier;
    bool activeStyle = true;

    private new void Awake()
    {
        base.Awake();

        weaponId = 0;

        AddListeners();

        if (chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();
        
        styleMultiplier = player.weapon.styleLevel == 0 ? 0.5f : player.weapon.styleLevel;
        EnableStyle();
    }

    private void OnDestroy()
    {
        RemoveListeners();

        if (chestSynergy) DisableChestSynergy();
        if (legSynergy) DisableLegSynergy();
    }

    #region Style
    private void Style(GameObject target,  GameObject user, CardData card)
    {
        if (user.GetComponent<Entity>() is not Player) return;

        if (!activeStyle) return;

        currentStyleAttacks--;

        if (currentStyleAttacks == 0) {
            activeStyle = false;
            player.AttackMultiplier(-styleMultiplier);
        }
    }
    private void EnableStyle() {
        player.AttackMultiplier(styleMultiplier);
    }
    private void Turn()
    {
        if (!activeStyle) player.AttackMultiplier(styleMultiplier);
        currentStyleAttacks = totalStyleAttacks;
        activeStyle = true;
    }
    #endregion

    #region Synergies
    private void EnableChestSynergy() {
        totalStyleAttacks = 1 + (chestSynergy ? (1 * chestLevel) + 1 : 0);
    }
    private void EnableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies)
            enemy.entityEffectsManager.UpdateEffectLimit(TAlteredEffects.AlteredEffects.Bleed, GetBleedUpgrade());
    }

    private void DisableChestSynergy() {

    }
    private void DisableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies)
            enemy.entityEffectsManager.UpdateEffectLimit(TAlteredEffects.AlteredEffects.Bleed, -GetBleedUpgrade());
    }

    private int GetBleedUpgrade()
    {
        switch (GetLegLevel())
        {
            case 0:
                return 1;
            case 1:
                return 3;
            case 2:
                return 5;
        }

        return 0;
    }
    #endregion

    #region Listeners
    private void AddListeners() {
        Damage.OnAttack += Style;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += Turn;
    }
    private void RemoveListeners() {
        Damage.OnAttack -= Style;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn -= Turn;
    }
    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con espada: el bonificador de da�o por estilo pasa a hacerse en los primeros {GameManager.Instance.playerData.chestArmor.synergyLevel+2} ataques.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con espada: aumenta el l�mite de cargas de sangrado de los enemigos a {5+(1+2*GameManager.Instance.playerData.legArmor.synergyLevel)} cargas.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: el primer ataque de cada turno hace un {(weapon.styleLevel == 0 ? 0.5f : weapon.styleLevel)*100}% m�s de da�o.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: el primer ataque de cada turno hace un {(styleLevel == 0 ? 0.5f : styleLevel) * 100}% m�s de da�o.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con espada: el bonificador de da�o por estilo pasa a hacerse en los primeros {synergyLevel + 2} ataques.";

        else
            return $"Sinergia con espada: aumenta el l�mite de cargas de sangrado de los enemigos a {5 + (1 + 2 * synergyLevel)} cargas.";
    }
    #endregion
}