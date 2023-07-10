using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Data")]
    [SerializeField] EntityData _entityData;

    [Header("Entity Stats")]
    public string entityName;
    public int currentHP;

    private void Awake()
    {
        setupEntity();
    }

    public bool sufferDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, _entityData.HP);
        return currentHP <= 0;
    }

    private void setupEntity()
    {
        entityName = _entityData.entityName;
        currentHP = _entityData.HP;
    }
}
