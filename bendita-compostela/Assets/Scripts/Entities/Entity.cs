using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Data")]
    [SerializeField] public EntityDataContainer entityDataContainer;
    [SerializeField] private EntityData entityData;

    [field:SerializeField] public string entityName { get; protected set; }
    [field:SerializeField] public int currentHP { get; protected set; }
    [field:SerializeField] public int damageMitigation { get; protected set; }
    [field:SerializeField] public int damageBoost { get; protected set; }
    [field:SerializeField] public int healingBoost { get; protected set; }

    private void Awake()
    {
        entityData = entityDataContainer.entityData;
    }

    #region Basic entity methods
    public void Turn()
    {

    }
    public bool sufferDamage(int damage, bool effect)
    {
        int finalDamage = effect ? damage : Mathf.Clamp(damage - damageMitigation, 0, damage);
        currentHP = Mathf.Clamp(currentHP - damage, 0, entityData.HP);
        return currentHP <= 0;
    }
    public void restoreHealth(int health, int boost, float multiplier)
    {
        var finalHeal = Mathf.RoundToInt(health * multiplier) + boost;
        currentHP = Mathf.Clamp(currentHP + finalHeal, 0, entityData.HP);
        return;
    }
    #endregion
}
