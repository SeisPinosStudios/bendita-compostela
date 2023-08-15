using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField, Header("Enemy Section")] EnemyData enemyData;

    private void Awake()
    {
        entityData = entityDataContainer.entityData;
        enemyData = (EnemyData)entityData;
        SetupEntity();
    }
    private void SetupEntity()
    {
        this.currentHP = enemyData.HP;
        this.defenseBonus = enemyData.damageMitigation;

        entityDisplay.UpdateHealth(entityData.HP, currentHP);
    }
}
