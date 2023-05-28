using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : BasicCardEffect
{
    new public static void effect(int damage)
    {
        Debug.Log("Damage card"+damage);
    }
}
