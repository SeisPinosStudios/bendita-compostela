using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }
    [field:SerializeField] public int styleAttackThreshold { get; private set; }
    [field: SerializeField] public float styleAttackMultiplier { get; private set; }
    [field:SerializeField] public int chestLevel { get; private set; }
    [field:SerializeField] public int legLevel { get; private set; }

    private void Awake()
    {
        weaponId = 1;
        player = BattleManager.Instance.player;

        styleAttackThreshold = player.weapon.styleLevel == 2 ? 4 : 5;
        styleAttackMultiplier = player.weapon.styleLevel == 0 ? 0.5f : 1.0f;

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
                enemy.SufferDamage(Mathf.RoundToInt(card.GetDamage()*styleAttackMultiplier), player.attackBonus, player.attackMultiplier, false);

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
    public static string GetStyleDescription()
    {
        return $"Estilo: al lanzar {(BattleManager.Instance.player.weapon.styleLevel == 2 ? 4 : 5)} tu siguiente ataque hará un " +
            $"{(BattleManager.Instance.player.weapon.styleLevel == 0 ? 0.5f : 1.0f)*100} de su daño al resto de enemigos.";
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
