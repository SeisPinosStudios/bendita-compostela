using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityEffectsManager : MonoBehaviour
{
    [SerializeField, Header("Entity Display variables")] Entity entity;
    [field: SerializeField] public EntityDisplay entityDisplay { get; private set; }
    [field:SerializeField] public Dictionary<TAlteredEffects.AlteredEffects, int> alteredEffects { get; private set; } = new Dictionary<TAlteredEffects.AlteredEffects, int>();
    [field:SerializeField] public Dictionary<TAlteredEffects.AlteredEffects, int> alteredEffectsLimit { get; private set; } = new Dictionary<TAlteredEffects.AlteredEffects, int>();
    [field:SerializeField] public Dictionary<CardData, int> frenzyAttacks { get; private set; } = new Dictionary<CardData, int>();
    [field:SerializeField] public List<TAlteredEffects.AlteredEffects> resistances { get; private set; }
    public int accumPoison = 1;
    [field: SerializeField] public int burnThreshold { get; private set; } = 6;
    [field: SerializeField] public float vulnerableMultiplier { get; private set; } = 0.5f;
    [field: SerializeField] public float guardedMultiplier { get; private set; } = 0.5f;
    /*====EVENTS====*/
    public event Action<TAlteredEffects.AlteredEffects, int> OnEffectApplied = delegate { };

    private void Awake()
    {
        SetupEffects();
    }

    #region Basic methods
    private void SetupEffects()
    {
        for (int i = 0; i < Enum.GetNames(typeof(TAlteredEffects.AlteredEffects)).Length; i++)
        {
            alteredEffects.Add((TAlteredEffects.AlteredEffects)i, 0);
            alteredEffectsLimit.Add((TAlteredEffects.AlteredEffects)i, TAlteredEffects.alteredEffectsLimit[(TAlteredEffects.AlteredEffects)i]);
        }
    }
    public void ApplyEffect(TAlteredEffects.AlteredEffects effect, int value)
    {
        if (resistances.Contains(effect)) return;
        alteredEffects[effect] = Mathf.Clamp(alteredEffects[effect] + value, 0, alteredEffectsLimit[effect]);
        OnEffectApplied(effect, value);
        UpdateEffects();
    }
    public void RemoveEffect(TAlteredEffects.AlteredEffects effect, int value)
    {
        alteredEffects[effect] = Mathf.Clamp(alteredEffects[effect] - value, 0, alteredEffectsLimit[effect]);
        UpdateEffects();
    }
    public void Cleanse()
    {
        foreach(KeyValuePair<TAlteredEffects.AlteredEffects, int> effect in alteredEffects) alteredEffects[effect.Key] = 0;
    }
    private void UpdateEffects()
    {
        if (alteredEffects[TAlteredEffects.AlteredEffects.Poison] == 0) accumPoison = 1;
        entityDisplay.UpdateAlteredEffectsDisplay(this);
    }
    public void AddResistance(TAlteredEffects.AlteredEffects effect)
    {
        resistances.Add(effect);
    }
    public void VulnerableMultiplier(float amount) { vulnerableMultiplier += amount; }
    public void GuardedMultiplier(float amount) { guardedMultiplier += amount; }
    public void SetBurnThreshold(int value)
    {
        burnThreshold = value;
    }
    public void UpdateEffectLimit(TAlteredEffects.AlteredEffects effect, int amount)
    {
        alteredEffectsLimit[effect] += amount;
    }
    #endregion

    #region Effect Methods
    public void Effect(TAlteredEffects.AlteredEffects effect)
    {
        Type.GetType(effect.ToString()).GetMethod("Effect").Invoke(null, new object[] { this, entity, this.gameObject, null });
        UpdateEffects();
    }
    public void Effect(TAlteredEffects.AlteredEffects effect, System.Object data)
    {
        Type.GetType(effect.ToString()).GetMethod("Effect").Invoke(null, new object[] { this, entity, this.gameObject, data });
        UpdateEffects();
    }
    public void Poison()
    {
        if (Suffering(TAlteredEffects.AlteredEffects.Poison))
            Effect(TAlteredEffects.AlteredEffects.Poison);
    }
    public void Bleed()
    {
        if (Suffering(TAlteredEffects.AlteredEffects.Bleed))
            Effect(TAlteredEffects.AlteredEffects.Bleed);
    }
    public void Burn()
    {
        if (Suffering(TAlteredEffects.AlteredEffects.Burn))
            Effect(TAlteredEffects.AlteredEffects.Burn);
    }
    #endregion

    #region Check methods
    public bool Suffering(TAlteredEffects.AlteredEffects effect)
    {
        return alteredEffects[effect] > 0;
    }
    #endregion
}
