using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Bendita Compostela/Armor")]
public class ArmorData : CardData
{
    [field:SerializeField, Header("Armor Section")] public int defenseBonus { get; private set; }
    [field: SerializeField] public int armorId { get; private set; }
    [field:SerializeField, Header("Weapon Synergy")] public int weaponSynergy { get; private set; }
    [field: SerializeField] public WeaponTypes weaponSynergyClass { get; private set; }
    [field:SerializeField, Header("Upgrades")] public int synergyLevel { get; private set; }
    [field:SerializeField] public int armorLevel { get; private set; }
    [field:SerializeField, Header("Equipment Screen")] public Sprite equipmentScreenIcon { get; private set; }
    [field:SerializeField] public string equipmentScreenDescription { get; private set; }
    [field: SerializeField] public int armorType { get; private set; }

    public new ArmorData Copy()
    {
        ArmorData card = CreateInstance<ArmorData>();

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
        card.defenseBonus = defenseBonus;
        card.weaponSynergy = weaponSynergy;
        card.synergyLevel = synergyLevel;
        card.armorLevel = armorLevel;
        card.armorId = armorId;
        card.equipmentScreenIcon = equipmentScreenIcon;
        card.equipmentScreenDescription = equipmentScreenDescription;
        card.armorType = armorType;
        card.weaponSynergyClass = weaponSynergyClass;

        return card;
    }
    public void UpgradeSynergyLevel()
    {
        synergyLevel++;
    }
    public void UpgradeArmorLevel()
    {
        defenseBonus++;
        armorLevel++;
    }
}
