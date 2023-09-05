using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marked : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"Al usar un ataque contra este objetivo, otro enemigo aleatorio recibirá un 50% del daño del ataque.";
    }
}
