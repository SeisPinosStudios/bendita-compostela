using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "BenditaCompostela/New card")]
public class CardData : ScriptableObject
{
    public enum Effect
    {
        Damage, Heal, DrawCards, EquipWeapon, RestoreEnergy, Cleanse, ApplyEffect, SelfApplyEffect
    }

    [Header("Card Info")]
    public string cardName;
    [TextArea(5,10)]
    public string description;
    public int cost, price;
    public Sprite art, miniArt;

    [Header("Card Effects")]
    public List<Effect> cardEffects;
    public List<string> cardEffectsValues;
    public bool printArrow;

    public int GetDamage()
    {
        if (!cardEffects.Contains(Effect.Damage)) return 0;
        return int.Parse(cardEffectsValues[cardEffects.IndexOf(Effect.Damage)]);
    }
    public string GetHeal()
    {
        if (!cardEffects.Contains(Effect.Heal)) return null;
        return cardEffectsValues[cardEffects.IndexOf(Effect.Heal)];
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

        return card;
    }
}
