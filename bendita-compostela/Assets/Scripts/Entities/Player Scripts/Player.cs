using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [field:SerializeField, Header("Player Specifics")] public PlayerData playerData { get; private set; }
    [field:SerializeField] public int energy { get; private set; }
    [field:SerializeField] public WeaponData weapon { get; private set; }
    
    private void Awake()
    {
        entityData = entityDataContainer.entityData;
        playerData = (PlayerData)entityDataContainer.entityData;
        EntitySetup();
    }
    private void EntitySetup()
    {
        this.currentHP = playerData.HP;
        this.defenseBonus = playerData.chestArmor.defenseBonus + playerData.legArmor.defenseBonus;

        entityDisplay.UpdateHealth(entityData.HP, currentHP);
    }

    #region Basic player methods
    public void RestoreEnergy(int amount)
    {
        this.energy = Mathf.Clamp(this.energy + amount, 0, 10);
        return;
    }
    public void SetWeapon(WeaponData weapon)
    {
        this.weapon = weapon;
    }
    #endregion
}
