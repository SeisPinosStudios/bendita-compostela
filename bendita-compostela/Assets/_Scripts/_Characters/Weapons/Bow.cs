using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    [field: SerializeField] public int synergyDraws { get; private set; }
    [field: SerializeField] public bool activeStyle { get; private set; } = false;
    private new void Awake()
    {
        base.Awake();

        weaponId = 5;

        AddListeners();

        if (chestSynergy) EnableChestSynergy();
        if (legSynergy) EnableLegSynergy();
    }

    private void OnDestroy() {
        RemoveListeners();

        if (chestSynergy) DisableChestSynergy();
        if (legSynergy) DisableLegSynergy();
    }

    #region Style
    private void Style(GameObject target, GameObject user, CardData card) {
        if (user.GetComponent<Entity>().GetType() != typeof(Player)) return;

        var enemy = target.GetComponent<Enemy>();
        var enemyIndex = BattleManager.Instance.enemies.IndexOf(enemy) + (3 - (BattleManager.Instance.enemies.Count - 1));
        var damageBonus = 3 - enemyIndex;

        enemy.SufferDamage(damageBonus, 0, 0, true);
    }
    #endregion

    #region Synergies
    /* Enablers */
    private void EnableChestSynergy() {
        if(chestLevel == 2) AttackDeckManager.Instance.AddFreeDraw(1);
        AttackDeckManager.Instance.OnCardDraw += ChestSynergy;
    }
    private void EnableLegSynergy() {
        player.AddMaxEnergy(2 * (GetLegLevel() + 1));
    }

    /* Disablers */
    private void DisableChestSynergy() {
        AttackDeckManager.Instance.OnCardDraw -= ChestSynergy;
    }
    private void DisableLegSynergy() {
        player.AddMaxEnergy(-2 * (GetLegLevel() + 1));
    }

    /* Synergy Methods */
    private void ChestSynergy() {
        if (activeStyle && GetChestLevel() != 2) { activeStyle = false; return; }
        if (synergyDraws < 1 - GetChestLevel()) { synergyDraws++; return; }
        AttackDeckManager.Instance.AddFreeDraw(1);
        synergyDraws = 0;
        activeStyle = true;
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
        if (GameManager.Instance.playerData.chestArmor.synergyLevel == 2) return "Sinergia con arco: tus robos del mazo de ataques no cuestan energía.";
        return $"Sinergia con arco: cada {2 - GameManager.Instance.playerData.chestArmor.synergyLevel} robos del mazo de ataques, tu siguiente robo es gratis.";
    }
    public static string GetLegDescription()
    {
        return $"Sinergia con arco: aumenta tu energ�a máxima {2 * (GameManager.Instance.playerData.legArmor.synergyLevel + 1)} puntos.";
    }
    public static string GetStyleDescription(WeaponData weapon)
    {
        return $"Estilo: Tus ataques hacen daño adicional cuanto más cerca esté el enemigo de ti. Sólo se aplica con varios enemigos.";
    }
    public static string GetStyleDescriptionByLevel(int styleLevel)
    {
        return $"Estilo: Tus ataques hacen daño adicional cuanto más cerca esté el enemigo de ti. Sólo se aplica con varios enemigos.";
    }
    public static string GetSynergyDescriptionByLevel(int synergyLevel, int armorType)
    {
        if (armorType == 0)
            if (synergyLevel == 2) return "Sinergia con arco: tus robos del mazo de ataques no cuestan energía.";
            else return $"Sinergia con arco: cada {2 - synergyLevel} robos del mazo de ataques, tu siguiente robo es gratis.";

        else
            return $"Sinergia con arco: aumenta tu energía máxima {2 * (synergyLevel + 1)} puntos.";
    }
    #endregion
}
