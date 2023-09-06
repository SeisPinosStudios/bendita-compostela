using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Bendita Compostela/Weapons/Weapon")]
public class WeaponData : CardData
{
    [field: SerializeField, Header("Weapon Info")] public int weaponId { get; private set; }
    [field: SerializeField] public BaseWeapon.Weapons weaponClassName { get; private set; }
    [field:SerializeField, Header("Weapon Attacks")] public List<WeaponAttackData> attacks { get; private set; }
    [field:SerializeField] public WeaponAttackData ultimate { get; private set; }
    [field:SerializeField, Header("Weapon Improvements")] public int weaponLevel { get; private set; }
    [field:SerializeField] public int styleLevel { get; private set; }
    [field:SerializeField] public int ultimateLevel { get; private set; }

    public void UpgradeWeaponLevel()
    {
        weaponLevel += 1;
        for (int i = 0; i < attacks.Count; i++) attacks[i] = attacks[i].improvedAttack;
    }
    public void UpgradeStyleLevel()
    {
        styleLevel += 1;
    }
    public void UpgradeUltimateLevel()
    {
        ultimateLevel += 1;
        if (ultimateLevel > 1) ultimate = ultimate.improvedAttack;
    }
    public bool IsUlti(WeaponAttackData attack)
    {
        return ultimate.cardName == attack.cardName;
    }

    public new WeaponData Copy()
    {
        WeaponData card = CreateInstance<WeaponData>();

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
        card.weaponId = weaponId;
        card.weaponClassName = weaponClassName;
        card.attacks = attacks;
        card.ultimate = ultimate;
        card.weaponLevel = weaponLevel;
        card.styleLevel = styleLevel;
        card.ultimateLevel = ultimateLevel;

        return card;
    }
}
