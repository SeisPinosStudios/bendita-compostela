using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Bendita Compostela/Armor")]
public class ArmorData : CardData
{
    [field:SerializeField, Header("Armor section")] public int defenseBonus { get; private set; }
    [field:SerializeField] public int weaponSynergy { get; private set; }
}
