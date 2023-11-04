using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    [field: SerializeField] public int styleAttacks { get; private set; }
    [field: SerializeField] public int styleAttacksThreshold { get; private set; }
    [field: SerializeField] public int styleRecover { get; private set; }
    [field: SerializeField] public int synergyDraws { get; private set; }
    [field: SerializeField] public bool activeStyle { get; private set; } = false;
    private void Awake()
    {
        weaponId = 5;
        Damage.OnAttack += Style;
        AttackDeckManager.Instance.OnCardDraw += ChestSynergy;

        player = BattleManager.Instance.player;

        styleAttacksThreshold = GetStyleLevel() < 2 ? 5 : 4;
        styleRecover = GetStyleLevel() > 0 ? 4 : 3;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();

        if (chestSynergy && GetChestLevel() == 2) AttackDeckManager.Instance.AddFreeDraw(1);
        if (legSynergy) LegSynergy();
    }

    private void Style(GameObject target, CardData card)
    {
        if (target.GetComponent<Entity>().GetType() == typeof(Player)) return;

        if(styleAttacks < styleAttacksThreshold)
        {
            styleAttacks++;
            return;
        }

        styleAttacks = 0;
        player.RestoreEnergy(styleRecover);
    }

    private void ChestSynergy()
    {
        if (!chestSynergy) return;
        if (activeStyle && GetChestLevel() != 2) { activeStyle = false; return; }
        if (synergyDraws < 1 - GetChestLevel()) { synergyDraws++; return; }
        AttackDeckManager.Instance.AddFreeDraw(1);
        synergyDraws = 0;
        activeStyle = true;
    }

    private void LegSynergy()
    {
        player.AddMaxEnergy(2 * (GetLegLevel() + 1));
    }

    private void OnDestroy()
    {
        if (legSynergy) player.AddMaxEnergy(-2 * (GetLegLevel() + 1));
        Damage.OnAttack -= Style;
        AttackDeckManager.Instance.OnCardDraw -= ChestSynergy;
    }

    #region Description
    public static string GetChestDescription()
    {
        if (GameManager.Instance.playerData.chestArmor.synergyLevel == 2) return "Sinergia con arco: tus robos del mazo de ataques no cuestan energ�a.";
        return $"Sinergia con arco: cada {2 - GameManager.Instance.playerData.chestArmor.synergyLevel} robos del mazo de ataques, tu siguiente robo es gratis.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con arco: aumenta tu energ�a m�xima {2 * (GameManager.Instance.playerData.legArmor.synergyLevel + 1)} puntos.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: tu maestr�a con el arco permite recuperar {(weapon.styleLevel > 0 ? 4 : 3)} cada " +
            $"{(weapon.styleLevel < 2 ? 5 : 4)} ataques.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: tu maestr�a con el arco permite recuperar {(styleLevel > 0 ? 4 : 3)} cada " +
            $"{(styleLevel < 2 ? 5 : 4)} ataques.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            if (synergyLevel == 2) return "Sinergia con arco: tus robos del mazo de ataques no cuestan energ�a.";
            else return $"Sinergia con arco: cada {2 - synergyLevel} robos del mazo de ataques, tu siguiente robo es gratis.";

        else
            return $"Sinergia con arco: aumenta tu energ�a m�xima {2 * (synergyLevel + 1)} puntos.";
    }
    #endregion
}
