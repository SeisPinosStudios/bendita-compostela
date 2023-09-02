using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }
    [field:SerializeField] public int styleAttackThreshold { get; private set; }
    [field:SerializeField] public int chestLevel { get; private set; }
    [field:SerializeField] public int legLevel { get; private set; }

    private void Awake()
    {
        weaponId = 1;
        player = BattleManager.Instance.player;

        styleAttackThreshold = player.weapon.styleLevel == 2 ? 4 : 5;

        chestSynergy = player.playerData.chestArmor.weaponSynergy == weaponId;
        legSynergy = player.playerData.legArmor.weaponSynergy == weaponId;
        chestLevel = player.playerData.chestArmor.synergyLevel;
        legLevel = player.playerData.legArmor.synergyLevel;

        HammerCard.OnHammerCard += Style;

        if (chestSynergy) ChestSynergy();
        if (legSynergy) LegSynergy();
    }
    private void ResetStyle()
    {
        styleAttacks = 0;
    }

    private void Style(CardData card, GameObject target)
    {
        if(styleAttacks < styleAttackThreshold) { styleAttacks++; return; }

        if (card.GetDamage() <= 0) return;

        var enemies = BattleManager.Instance.enemies;
        foreach (Enemy enemy in enemies) 
            if (enemy != target.GetComponent<Enemy>()) 
                enemy.SufferDamage(card.GetDamage(), player.damageBonus, player.damageMultiplier, false);

        ResetStyle();
    }

    private void ChestSynergy()
    {
        player.GetComponent<EntityEffectsManager>().VulnerableMultiplier(GetPlayerMultiplier());
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.GetComponent<EntityEffectsManager>().VulnerableMultiplier(GetEnemyMultiplier());
    }

    private void LegSynergy()
    {
        player.AttackBonus(GetDamageBonus());
    }

    private void OnDestroy()
    {
        if (chestSynergy)
        {
            player.GetComponent<EntityEffectsManager>().VulnerableMultiplier(-GetPlayerMultiplier());
            foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.GetComponent<EntityEffectsManager>().VulnerableMultiplier(-GetEnemyMultiplier());
        }

        if (legSynergy)
        {
            player.AttackBonus(-GetDamageBonus());
        }
    }

    private float GetEnemyMultiplier()
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
    private float GetPlayerMultiplier()
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
    private int GetDamageBonus()
    {
        return legLevel == 0 ? 1 : legLevel * 2;
    }
}
