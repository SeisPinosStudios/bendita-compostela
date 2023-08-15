using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Bendita Compostela/Weapon")]
public class WeaponData : CardData
{
    [field:SerializeField, Header("Weapon section")] public List<CardData> attacks { get; private set; }
    [field:SerializeField] public int weaponId { get; private set; }
    [field:SerializeField] public BaseWeapon.Weapons weaponClassName { get; private set; }
    [field:SerializeField] public int weaponLevel { get; private set; }
    [field:SerializeField] public int styleLevel { get; private set; }
    [field:SerializeField] public int ultimateLevel { get; private set; }
}
