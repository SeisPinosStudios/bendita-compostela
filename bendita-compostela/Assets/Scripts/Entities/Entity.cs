using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Data")]
    [SerializeField] public EntityDataContainer entityDataContainer;
    [field:SerializeField] public EntityData entityData { get; protected set; }
    [field:SerializeField] public EntityDisplay entityDisplay { get; protected set; }
    [field:SerializeField] public string entityName { get; protected set; }
    [field:SerializeField] public int currentHP { get; protected set; }
    [field:SerializeField] public int defenseBonus { get; protected set; }
    [field:SerializeField] public int attackBonus { get; protected set; }
    [field:SerializeField] public int healingBonus { get; protected set; }
    [field:SerializeField] public float damageMitigation { get; protected set; }
    [field:SerializeField] public float damageBoost { get; protected set; }
    [field:SerializeField] public float healingBoost { get; protected set; }

    private void Awake()
    {

    }

    #region Basic entity methods
    public bool SufferDamage(int damage, bool effect)
    {
        int finalDamage = effect ? damage : Mathf.Clamp(damage - defenseBonus, 0, damage);
        currentHP = Mathf.Clamp(currentHP - damage, 0, entityData.HP);

        entityDisplay.UpdateHealth(entityData.HP, currentHP);

        return currentHP <= 0;
    }
    public void RestoreHealth(int health, int boost, float multiplier)
    {
        var finalHeal = Mathf.RoundToInt(health * multiplier) + boost;
        currentHP = Mathf.Clamp(currentHP + finalHeal, 0, entityData.HP);

        entityDisplay.UpdateHealth(entityData.HP, currentHP);

        return;
    }
    public void DamageBoost(float amount) { damageBoost += amount; }
    public void DamageMitigation(float amount) { damageMitigation += amount; }
    public void DefenseBonus(int amount) { defenseBonus += amount; }
    public void AttackBonus(int amount) { attackBonus += amount; }
    #endregion

    #region Entity status methods
    public bool IsDamaged(float percentage)
    {
        return currentHP < entityData.HP * percentage;
    }
    #endregion
}
