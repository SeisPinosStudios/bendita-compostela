using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack", menuName ="Bendita Compostela/Weapons/Attack")]
public class WeaponAttackData : CardData
{
    [field: SerializeField, Header("Weapon Attack")] public WeaponAttackData improvedAttack { get; private set; }

    public new WeaponAttackData Copy()
    {
        WeaponAttackData card = CreateInstance<WeaponAttackData>();
        
        card.cardName = cardName;
        card.description = description;
        card.cost = cost;
        card.price = price;
        card.art = art;
        card.miniArt = miniArt;
        card.cardEffects = cardEffects;
        card.cardEffectsValues = cardEffectsValues;
        card.printArrow = printArrow;
        card.improvedAttack = improvedAttack ? improvedAttack.Copy() : null;
        card.name = name;

        return card;
    }
}
