using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    [Header("Entity Classes")]
    [SerializeField] public EntityDataContainer entityDataContainer;
    [field: SerializeField] public EntityData entityData { get; protected set; }
    [field: SerializeField] public EntityDisplay entityDisplay { get; protected set; }
    [field: SerializeField] public EntityEffectsManager entityEffectsManager { get; protected set; }
    [field: SerializeField] public EntityBehaviour entityBehaviour { get; protected set; }
    [field: SerializeField, Header("Entity Data")] public string entityName { get; protected set; }
    [field: SerializeField] public int currentHP { get; protected set; }
    [field: SerializeField, Header("Stat Bonus")] public int defenseBonus { get; protected set; }
    [field: SerializeField] public int attackBonus { get; protected set; }
    [field: SerializeField] public int healingBonus { get; protected set; }
    [field: SerializeField, Header("Stat Multiplier")] public float defenseMultiplier { get; protected set; } = 1.0f;
    [field: SerializeField] public float attackMultiplier { get; protected set; } = 1.0f;
    [field: SerializeField] public float healingMultiplier { get; protected set; } = 1.0f;
    /*===========EVENTS===========*/
    public event Action OnDamage = delegate { };
    public event Action<int> OnDamaged = delegate { };

    private void Awake()
    {

    }

    #region Basic entity methods
    public void SufferDamage(int damage, int damageBonus, float damageMultiplier, bool effect)
    {
        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Invulnerable))
        {
            entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Invulnerable, 1);
            return;
        }

        OnDamage();

        var finalDamage = damage;

        if (!effect)
        {
            finalDamage = Mathf.RoundToInt((damage + damageBonus - defenseBonus) * damageMultiplier / ComputeDefenseMultiplier());
            OnDamaged(finalDamage);
        }

        currentHP = Mathf.Clamp(currentHP - finalDamage, 0, entityData.HP);

        Debug.Log($"damageBonus {damageBonus} | damageMultiplier {damageMultiplier} | defenseBonus {defenseBonus} | defenseMultiplier {defenseMultiplier}");
        Debug.Log($"Damaged {name} for {finalDamage} damage");
        entityDisplay.UpdateHealth(entityData.HP, currentHP);
        entityDisplay.HitAnimation();
        BattleManager.Instance.soundList.PlaySound("Slash");
        CheckDeath();
        return;
    }
    private float ComputeDefenseMultiplier()
    {
        var finalDefenseMultiplier = defenseMultiplier;

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Guarded))
        {
            finalDefenseMultiplier += entityEffectsManager.guardedMultiplier;
            entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Guarded, 1);
        }

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Vulnerable))
        {
            finalDefenseMultiplier -= entityEffectsManager.vulnerableMultiplier;
            entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Vulnerable, 1);
            entityDisplay.Vulnerable();
            BattleManager.Instance.soundList.PlaySound("Vulnerable");
        }

        return finalDefenseMultiplier;
    }
    public float ComputeAttackMultiplier()
    {
        var finalAttackMultiplier = attackMultiplier;

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Lead))
        {
            finalAttackMultiplier -= 0.5f;
            entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Lead, 1);
        }

        return finalAttackMultiplier;
    }
    public float GetDefenseMultiplier()
    {
        var multiplier = defenseMultiplier;

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Guarded))
            multiplier += entityEffectsManager.guardedMultiplier;

        if(entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Vulnerable))
            multiplier -= entityEffectsManager.vulnerableMultiplier;

        return multiplier;
    }
    public float GetAttackMultiplier()
    {
        var multiplier = attackMultiplier;

        if (entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Lead))
            multiplier -= 0.5f;

        return multiplier;
    }
    public void RestoreHealth(int health, int bonus, float multiplier)
    {
        var finalHeal = Mathf.RoundToInt((health + bonus) * multiplier);
        currentHP = Mathf.Clamp(currentHP + finalHeal, 0, entityData.HP);

        entityDisplay.UpdateHealth(entityData.HP, currentHP);
        BattleManager.Instance.soundList.PlaySound("Heal");

        return;
    }
    protected virtual IEnumerator Death()
    {
        yield return null;
    }
    #endregion

    #region Entity status methods
    public bool IsDamaged(float percentage)
    {
        return currentHP < entityData.HP * percentage;
    }
    public bool IsDead()
    {
        return currentHP <= 0;
    }
    public void CheckDeath()
    {
        if (currentHP <= 0) StartCoroutine(Death());
    }
    #endregion

    #region Entity stats methods
    public void AttackMultiplier(float amount) { attackMultiplier += amount; }
    public void DefenseMultiplier(float amount) { defenseMultiplier += amount; }
    public void HealingMultiplier(float amount) { healingMultiplier += amount; }
    public void DefenseBonus(int amount) { defenseBonus += amount; }
    public void AttackBonus(int amount) { attackBonus += amount; }
    public void HealingBonus(int amount) { healingBonus += amount; }
    #endregion
}
