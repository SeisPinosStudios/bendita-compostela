using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BasicCardEffect
{
    new public static void effect(int heal)
    {
        Debug.Log("Healing card");
    }
}
