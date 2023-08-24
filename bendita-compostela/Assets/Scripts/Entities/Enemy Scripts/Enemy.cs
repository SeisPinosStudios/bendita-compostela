using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Entity
{
    [SerializeField, Header("Enemy Section")] EnemyData enemyData;
    public event Action OnDeath;

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

        foreach (BasicPassive.Passive passive in enemyData.passives) gameObject.AddComponent(Type.GetType(passive.ToString()));

        entityDisplay.UpdateHealth(entityData.HP, currentHP);
    }
    protected new IEnumerator Death()
    {
        OnDeath();
        yield return null;
    }
}
