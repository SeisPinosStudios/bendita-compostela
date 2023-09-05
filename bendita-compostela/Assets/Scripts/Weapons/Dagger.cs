using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BaseWeapon
{
    [field: SerializeField] public int styleBonus { get; private set; }
    [field: SerializeField] public int styleBonusAccum { get; private set; }
    [field: SerializeField] public static bool synergyEffect { get; private set; }
    [field: SerializeField] public EntityEffectsManager playerEffectsManager { get; private set; }
    [field: SerializeField] public int chestLevel { get; private set; }
    private void Awake()
    {
        weaponId = 2;
        Damage.OnAttack += Style;
        BattleManager.Instance.OnBattleEnd += ResetSynergyEffect;

        player = BattleManager.Instance.player;
        playerEffectsManager = player.entityEffectsManager;
        playerEffectsManager.OnEffectApplied += SynergyEffect;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += ResetStyle;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();
        chestLevel = GetChestLevel();

        styleBonus = 1 + (1 * GetStyleLevel());
    }

    private void Style(GameObject target, CardData card)
    {
        if (target.GetComponent<Entity>() is Player) return;
        if (BattleManager.Instance.enemies.Count > 1) return;
        styleBonusAccum += styleBonus;
        BattleManager.Instance.player.AttackBonus(styleBonus);
    }
    private void SynergyEffect(TAlteredEffects.AlteredEffects effect, int value)
    {
        if (effect != TAlteredEffects.AlteredEffects.Poison || !synergyEffect) return;
        
        playerEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Poison, value);
        
        foreach (Enemy enemy in BattleManager.Instance.enemies)
            enemy.GetComponent<EntityEffectsManager>().ApplyEffect(TAlteredEffects.AlteredEffects.Poison, 1 + (1 * chestLevel));
        
        synergyEffect = false;
    }
    private void ResetSynergyEffect()
    {
        synergyEffect = true;
    }
    private void ResetStyle()
    {
        player.AttackBonus(-styleBonusAccum);
    }
    private void OnDestroy()
    {
        BattleManager.Instance.player.AttackBonus(-styleBonusAccum);
        playerEffectsManager.OnEffectApplied -= SynergyEffect;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn -= ResetStyle;
        Damage.OnAttack -= Style;
        BattleManager.Instance.OnBattleEnd -= ResetSynergyEffect;
    }

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
    public static string GetStyleDescription()
    {
        return $"Estilo: cuando sólo queda un enemigo, cada ataque de daga aumenta tu daño con todos los ataques {BattleManager.Instance.player.weapon.styleLevel + 1} " +
            $"hasta el final del turno.";
    }
    #endregion
}
