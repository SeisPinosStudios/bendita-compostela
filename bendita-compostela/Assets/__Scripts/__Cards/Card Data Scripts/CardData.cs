using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "BenditaCompostela/New card")]
public class CardData : ScriptableObject
{
    public enum Effect
    {
        Damage, Heal, DrawCards, EquipWeapon, RestoreEnergy, Cleanse, ApplyEffect, SelfApplyEffect, HammerCard, HealOthers, DamageAll, ApplyEffectAll, Special,
        DrawAttack
    }

    [Header("Card Info")]
    public string cardName;
    [field: SerializeField] public int cardId { get; protected set; }
    [TextArea(5,10)]
    public string description;
    public int cost, price;
    public Sprite art, miniArt;
    [field: SerializeField] public bool attack { get; private set; }

    [Header("Card Effects")]
    public List<Effect> cardEffects;
    public List<string> cardEffectsValues;
    public bool printArrow;

    public int GetDamage()
    {
        if (!cardEffects.Contains(Effect.Damage)) return 0;
        return int.Parse(cardEffectsValues[cardEffects.IndexOf(Effect.Damage)]);
    }
    public int GetDamageAll()
    {
        if (!cardEffects.Contains(Effect.DamageAll)) return 0;
        return int.Parse(cardEffectsValues[cardEffects.IndexOf(Effect.Damage)]);
    }
    public string GetHeal()
    {
        if (!cardEffects.Contains(Effect.Heal)) return null;
        return cardEffectsValues[cardEffects.IndexOf(Effect.Heal)];
    }
    public List<string> GetAlteredEffects()
    {
        var effects = new List<string>();
        for (int i = 0; i < cardEffects.Count; i++) if (cardEffects[i] == Effect.ApplyEffect) effects.Add(cardEffectsValues[i]);
        return effects;
    }
    public List<string> GetEffect(Effect effect)
    {
        var effects = new List<string>();
        for (int i = 0; i < cardEffects.Count; i++) if (cardEffects[i] == effect) effects.Add(cardEffectsValues[i]);
        return effects;
    }
    public bool AppliesEffect(TAlteredEffects.AlteredEffects effect)
    {
        for (int i = 0; i < cardEffects.Count; i++)
            if (cardEffects[i] == Effect.ApplyEffect)
                if (cardEffectsValues[i].Split("|")[0] == effect.ToString())
                    return true;
        
        return false;
    }
    public bool IsAttack()
    {
        return attack;
    }
    public void Cost(int amount)
    {
        cost += amount;
        Debug.Log($"Reduced card {name} cost by {amount}");
    }
    public virtual CardData Copy()
    {
        CardData card = CreateInstance<CardData>();

        card.name = name;
        card.cardName = cardName;
        card.description = description;
        card.cost = cost;
        card.price = price;
        card.art = art;
        card.miniArt = miniArt;
        card.cardEffects = cardEffects;
        card.cardEffectsValues = cardEffectsValues;
        card.printArrow = printArrow;
        card.attack = attack;

        return card;
    }
}
