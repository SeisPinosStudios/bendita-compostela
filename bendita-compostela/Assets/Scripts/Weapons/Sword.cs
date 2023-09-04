using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }
    [field:SerializeField] public int chestLevel { get; private set; }
    [field:SerializeField] public float styleMultiplier { get; private set; }

    private void Awake()
    {
        weaponId = 0;
        player = GetComponent<Player>();
        chestSynergy = player.playerData.chestArmor.weaponSynergy == weaponId;
        legSynergy = player.playerData.legArmor.weaponSynergy == weaponId;
        chestLevel = player.playerData.chestArmor.synergyLevel;
        

        styleMultiplier = player.weapon.styleLevel == 0 ? 0.5f : player.weapon.styleLevel;

        Damage.OnAttack += Style;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += Turn;
        Turn();
        if (legSynergy) LegSynergy();
    }

    #region Style
    private void Style(GameObject target, CardData card)
    {
        if (TurnManager.Instance.entityTurn.GetType() != typeof(PlayerBehaviour)) return;

        if (styleAttacks > 0) { styleAttacks--; return; }

        player.AttackMultiplier(-styleMultiplier);
    }
    private void Turn()
    {
        if (styleAttacks <= 0) player.AttackMultiplier(styleMultiplier);
        styleAttacks = 1 + (chestSynergy ? (1 * chestLevel) + 1 : 0);
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

    private void OnDestroy()
    {
        Damage.OnAttack -= Style;

        if(legSynergy)
            foreach (Enemy enemy in BattleManager.Instance.enemies)
                enemy.entityEffectsManager.UpdateEffectLimit(TAlteredEffects.AlteredEffects.Bleed, -GetBleedUpgrade());
    }
}
