using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Bendita Compostela/Armor")]
public class ArmorData : CardData
{
    [field:SerializeField, Header("Armor section")] public int defenseBonus { get; private set; }
    [field:SerializeField] public int weaponSynergy { get; private set; }
    [field:SerializeField] public int synergyLevel { get; private set; }
    [field:SerializeField] public int styleLevel { get; private set; }

    public new ArmorData Copy()
    {
        ArmorData card = CreateInstance<ArmorData>();

        card.cardName = cardName;
        card.description = description;
        card.cost = cost;
        card.price = price;
        card.art = art;
        card.miniArt = miniArt;
        card.cardEffects = cardEffects;
        card.cardEffectsValues = cardEffectsValues;
        card.printArrow = printArrow;
        card.defenseBonus = defenseBonus;
        card.weaponSynergy = weaponSynergy;
        card.synergyLevel = synergyLevel;
        card.styleLevel = styleLevel;

        return card;
    }
}
