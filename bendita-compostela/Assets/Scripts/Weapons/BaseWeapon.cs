using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public enum Weapons { Sword, Hammer, Dagger, Spear, Staff, Bow}
    [field: SerializeField] public int weaponId { get; protected set; }
    [field: SerializeField] public Player player { get; protected set; }
    [field: SerializeField] public bool chestSynergy { get; protected set; }
    [field: SerializeField] public bool feetSynergy { get; protected set; }
    
}
