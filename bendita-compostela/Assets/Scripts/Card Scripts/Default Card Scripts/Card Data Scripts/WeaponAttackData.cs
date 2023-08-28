using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack", menuName ="Bendita Compostela/Weapons/Attack")]
public class WeaponAttackData : CardData
{
    [field: SerializeField, Header("Weapon Attack")] public WeaponAttackData improvedAttack { get; private set; }
}
