using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=6> Exhausto: Las cartas que juegue cuesta 1 punto más de energía.";
    }
}
