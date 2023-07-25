using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] PlayerData playerData;

    private void Awake()
    {
        playerData = (PlayerData)entityDataContainer.entityData;
        EntitySetup();
    }
    private void EntitySetup()
    {
        this.currentHP = playerData.HP;
        this.damageMitigation = playerData.chestArmor.damageMitigation + playerData.legArmor.damageMitigation;
    }
    public new void Turn()
    {
        DeckManager.Instance.DrawCard(5);
    }
}
