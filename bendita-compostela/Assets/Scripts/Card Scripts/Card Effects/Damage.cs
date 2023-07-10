using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : BasicCardEffect
{
    public static void effect(int damage, GameObject target)
    {
        Debug.Log("Damage card"+damage);
        target.GetComponent<Entity>().sufferDamage(damage);
    }
}
