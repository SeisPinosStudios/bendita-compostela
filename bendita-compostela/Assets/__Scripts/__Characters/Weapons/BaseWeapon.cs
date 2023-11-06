using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [field: SerializeField] public int weaponId { get; protected set; }
    [field: SerializeField] public WeaponData weaponData { get; protected set; }
    [field: SerializeField] public Player player { get; protected set; }
    protected bool chestSynergy;
    protected bool legSynergy;
    protected int chestLevel;
    protected int legLevel;
    protected int styleLevel;

    protected void Awake() {
        player = BattleManager.Instance.player;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();
        chestLevel = GetChestLevel();
        legLevel = GetLegLevel();
        styleLevel = GetStyleLevel();
    }

    #region Methods
    protected bool GetChestSynergy()
    {
        return player.playerData.chestArmor.weaponSynergy == weaponId;
    }
    protected bool GetLegSynergy()
    {
        return player.playerData.legArmor.weaponSynergy == weaponId;
    }
    protected int GetChestLevel()
    {
        return player.playerData.chestArmor.synergyLevel;
    }

    /// <summary>
    /// Gets the leg's armor synergy level.
    /// </summary>
    /// <returns>Leg's armor synergy level.</returns>
    protected int GetLegLevel()
    {
        return player.playerData.legArmor.synergyLevel;
    }

    /// <summary>
    /// Gets the weapon's style level.
    /// </summary>
    /// <returns>Weapon's style level.</returns>
    protected int GetStyleLevel()
    {
        return player.weapon.styleLevel;
    }
    #endregion
}

public enum WeaponTypes { 
    Sword, 
    Hammer, 
    Dagger, 
    Spear, 
    Staff, 
    Bow 
}
