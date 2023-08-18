using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    [Header("Entity Data")]
    [SerializeField] public EntityDataContainer entityDataContainer;
    [field: SerializeField] public EntityData entityData { get; protected set; }
    [field: SerializeField] public EntityDisplay entityDisplay { get; protected set; }
    [field: SerializeField] public string entityName { get; protected set; }
    [field: SerializeField] public int currentHP { get; protected set; }
    [field: SerializeField] public int defenseBonus { get; protected set; }
    [field: SerializeField] public int damageBonus { get; protected set; }
    [field: SerializeField] public int healingBonus { get; protected set; }
    [field: SerializeField] public float defenseMultiplier { get; protected set; }
    [field: SerializeField] public float damageMultiplier { get; protected set; }
    [field: SerializeField] public float healingMultiplier { get; protected set; }
    public event Action OnDamage = delegate { };

    private void Awake()
    {

    }

    #region Basic entity methods
    public void SufferDamage(int damage, bool effect)
    {
        OnDamage();
        int finalDamage = effect ? damage : Mathf.Clamp(damage - defenseBonus, 0, damage);
        currentHP = Mathf.Clamp(currentHP - damage, 0, entityData.HP);

        entityDisplay.UpdateHealth(entityData.HP, currentHP);

        return;
    }
    public void RestoreHealth(int health, int bonus, float multiplier)
    {
        var finalHeal = Mathf.RoundToInt(health * bonus) + bonus;
        currentHP = Mathf.Clamp(currentHP + finalHeal, 0, entityData.HP);

        entityDisplay.UpdateHealth(entityData.HP, currentHP);

        return;
    }
    public void DamageMultiplier(float amount) { damageMultiplier += amount; }
    public void DefenseMultiplier(float amount) { defenseMultiplier += amount; }
    public void DefenseBonus(int amount) { defenseBonus += amount; }
    public void AttackBonus(int amount) { damageBonus += amount; }
    protected IEnumerator Death()
    {
        yield return null;
    }
    #endregion

    #region Entity status methods
    public bool IsDamaged(float percentage)
    {
        return currentHP < entityData.HP * percentage;
    }
    public void CheckDeath()
    {
        if (currentHP <= 0) StartCoroutine(Death());
    }
    #endregion
}
