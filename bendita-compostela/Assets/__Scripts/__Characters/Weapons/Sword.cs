using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }
    [field:SerializeField] public float styleMultiplier { get; private set; }
    [field: SerializeField] public bool activeStyle { get; private set; } = true;

    private new void Awake()
    {
        base.Awake();

        weaponId = 0;
        
        styleMultiplier = player.weapon.styleLevel == 0 ? 0.5f : player.weapon.styleLevel;

        Damage.OnAttack += Style;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += Turn;
        Turn();
        if (legSynergy) LegSynergy();
    }

    private void OnDestroy()
    {
        Damage.OnAttack -= Style;

        if (styleAttacks > 0) player.AttackMultiplier(-styleMultiplier);

        if (legSynergy)
            foreach (Enemy enemy in BattleManager.Instance.enemies)
                enemy.entityEffectsManager.UpdateEffectLimit(TAlteredEffects.AlteredEffects.Bleed, -GetBleedUpgrade());
    }

    #region Style
    private void Style(GameObject target,  GameObject user, CardData card)
    {
        if (TurnManager.Instance.entityTurn.GetType() != typeof(PlayerBehaviour)) return;

        if (styleAttacks > 0) { styleAttacks--; return; }

        if(activeStyle) player.AttackMultiplier(-styleMultiplier);

        activeStyle = false;
    }
    private void Turn()
    {
        if (styleAttacks <= 0) player.AttackMultiplier(styleMultiplier);
        styleAttacks = 1 + (chestSynergy ? (1 * chestLevel) + 1 : 0);
        activeStyle = true;
    }
    #endregion

    #region Synergies
    private void LegSynergy()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) 
            enemy.entityEffectsManager.UpdateEffectLimit(TAlteredEffects.AlteredEffects.Bleed, GetBleedUpgrade());
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

    #endregion

    #region Description
    public static string GetChestDescription()
    {
        return $"Sinergia con espada: el bonificador de daño por estilo pasa a hacerse en los primeros {GameManager.Instance.playerData.chestArmor.synergyLevel+2} ataques.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con espada: aumenta el límite de cargas de sangrado de los enemigos a {5+(1+2*GameManager.Instance.playerData.legArmor.synergyLevel)} cargas.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: el primer ataque de cada turno hace un {(weapon.styleLevel == 0 ? 0.5f : weapon.styleLevel)*100}% más de daño.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: el primer ataque de cada turno hace un {(styleLevel == 0 ? 0.5f : styleLevel) * 100}% más de daño.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            return $"Sinergia con espada: el bonificador de daño por estilo pasa a hacerse en los primeros {synergyLevel + 2} ataques.";

        else
            return $"Sinergia con espada: aumenta el límite de cargas de sangrado de los enemigos a {5 + (1 + 2 * synergyLevel)} cargas.";
    }
    #endregion
}
