using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BaseWeapon
{
    [SerializeField] int styleBonus;
    [SerializeField] int styleBonusAccum;
    [SerializeField] bool synergyEffect;
    [SerializeField] public EntityEffectsManager playerEffectsManager;
    private new void Awake()
    {
        base.Awake();

        weaponId = 2;

        AddListeners();

        if (chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();

        playerEffectsManager = player.entityEffectsManager;

        styleBonus = 1 + (1 * styleLevel);
    }

    private void OnDestroy()
    {
        RemoveListeners();
        ResetStyle();

        if(chestSynergy) DisableChestSynergy();
        if(legSynergy) DisableLegSynergy();
    }

    #region Style
    private void Style(GameObject target, GameObject user, CardData card) {
        if (user.GetComponent<Entity>() is not Player || BattleManager.Instance.enemies.Count > 1) return;

        styleBonusAccum += styleBonus;
        BattleManager.Instance.player.AttackBonus(styleBonus);
    }
    private void ResetStyle() {
        player.AttackBonus(-styleBonusAccum);
    }
    #endregion

    #region Synergies
    private void EnableChestSynergy() {
        playerEffectsManager.OnEffectApplied += SynergyEffect;
        BattleManager.Instance.OnBattleEnd += ResetSynergyEffect;
    }
    private void EnableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.maxPoisonDamage = 8 + legLevel * 2;
    }

    private void DisableChestSynergy() {
        playerEffectsManager.OnEffectApplied -= SynergyEffect;
        BattleManager.Instance.OnBattleEnd -= ResetSynergyEffect;
    }
    private void DisableLegSynergy() {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.maxPoisonDamage = 5;
    }

    private void SynergyEffect(TAlteredEffects.AlteredEffects effect, int value) {
        if (effect != TAlteredEffects.AlteredEffects.Poison || !synergyEffect) return;

        playerEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Poison, value);

        foreach (Enemy enemy in BattleManager.Instance.enemies)
            enemy.GetComponent<EntityEffectsManager>().ApplyEffect(TAlteredEffects.AlteredEffects.Poison, 1 + (1 * chestLevel));

        synergyEffect = false;
    }
    private void ResetSynergyEffect() {
        synergyEffect = true;
    }
    #endregion

    #region Listeners
    private void AddListeners() {
        Damage.OnAttack += Style;
        
        
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += ResetStyle;
    }
    private void RemoveListeners() {
        Damage.OnAttack -= Style;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn -= ResetStyle;
    }
    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con daga: una vez por combate, al ser envenenado, te libras del efecto veneno y en cambio aplicas " +
            $"{GameManager.Instance.playerData.chestArmor.synergyLevel + 1} cargas de veneno a todos los enemigos.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con daga: aumenta el daño acumulable por veneno a {8 + GameManager.Instance.playerData.legArmor.synergyLevel * 2} puntos de daño.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: cuando sólo queda un enemigo, cada ataque de daga aumenta tu daño con todos los ataques {weapon.styleLevel + 1} " +
            $"hasta el final del turno.";
    }

    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: cuando sólo queda un enemigo, cada ataque de daga aumenta tu daño con todos los ataques {styleLevel + 1} " +
            $"hasta el final del turno.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con daga: una vez por combate, al ser envenenado, te libras del efecto veneno y en cambio aplicas " +
            $"{synergyLevel + 1} cargas de veneno a todos los enemigos.";

        else
            return $"Sinergia con daga: aumenta el daño acumulable por veneno a {8 + synergyLevel * 2} puntos de daño.";
    }
    #endregion
}
