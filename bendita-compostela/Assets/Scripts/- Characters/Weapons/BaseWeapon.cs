using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public enum Weapons { Sword, Hammer, Dagger, Spear, Staff, Bow}
    [field: SerializeField] public int weaponId { get; protected set; }
    [field: SerializeField] public WeaponData weaponData { get; protected set; }
    [field: SerializeField] public Player player { get; protected set; }
    [field: SerializeField] public bool chestSynergy { get; protected set; }
    [field: SerializeField] public bool legSynergy { get; protected set; }

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
    protected int GetLegLevel()
    {
        return player.playerData.legArmor.synergyLevel;
    }
    protected int GetStyleLevel()
    {
        return player.weapon.styleLevel;
    }
}
