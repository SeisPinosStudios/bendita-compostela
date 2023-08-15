using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }

    private void Awake()
    {
        weaponId = 2;
        HammerCard.onHammerCard += (() => styleAttacks++);
    }
    public void ResetStyle()
    {
        styleAttacks = 0;
    }
}
