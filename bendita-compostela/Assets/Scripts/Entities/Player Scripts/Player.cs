using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    [field:SerializeField, Header("Player Specifics")] public PlayerData playerData { get; private set; }
    [field:SerializeField] public int energy { get; private set; }
    [field: SerializeField] public int maxEnergy { get; private set; } = 10;
    [field:SerializeField] public WeaponData weapon { get; private set; }
    /*====ACTIONS====*/
    public event Action OnDeath = delegate { };
    public delegate void EnergyValueChanged(int currentEnergy);
    public event EnergyValueChanged OnEnergyValueChanged;
    
    private void Awake()
    {
        
    }

    public IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance.playerData);
        entityDataContainer.entityData = GameManager.Instance.playerData;
        entityData = GameManager.Instance.playerData;
        playerData = GameManager.Instance.playerData;
        entityDisplay.entityAnimator.runtimeAnimatorController = playerData.playerAnimator;
        EntitySetup();
    }

    #region Setup Methods
    private void EntitySetup()
    {
        currentHP = playerData.currentHP;
        defenseBonus = playerData.GetDefense();

        entityDisplay.UpdateHealth(entityData.HP, currentHP);
        GenerateCondecorations();
    }
    private void GenerateCondecorations()
    {
        foreach (CondecorationData condecoration in playerData.condecorations) gameObject.AddComponent(Type.GetType(condecoration.type.ToString()));
    }
    #endregion

    #region Basic Player Methods
    public void RestoreEnergy(int amount)
    {
        this.energy = Mathf.Clamp(this.energy + amount, 0, maxEnergy);
        if(OnEnergyValueChanged != null) OnEnergyValueChanged(this.energy);
        return;
    }
    public void AddMaxEnergy(int amount)
    {
        maxEnergy += amount;
        RestoreEnergy(amount);
    }
    public bool ConsumeEnergy(int amount)
    {
        if (energy < amount) { Debug.Log($"Not enough energy"); return false; }
        energy -= amount;
        if (OnEnergyValueChanged != null) OnEnergyValueChanged(energy);
        Debug.Log($"Enough energy to use card");
        return true;
    }
    public void SetWeapon(WeaponData weapon)
    {
        this.weapon = weapon;
    }
    protected override IEnumerator Death()
    {
        if(GetComponent<Friend>() != null && GetComponent<Friend>().active)
        {
            RestoreHealth(1, 0, 1);
            GetComponent<Friend>().active = false;
            yield break;
        }
        OnDeath();
        yield return null;
    }
    #endregion

    #region Check Methods
    
    #endregion
}
