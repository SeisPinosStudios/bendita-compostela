using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "BenditaCompostela/New armor")]
public class ArmorData : CardData
{
    [field:SerializeField] public int damageMitigation { get; private set; }
}
