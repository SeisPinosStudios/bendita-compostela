using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=2> Vulnerable: El próximo ataque que reciba hará un {entityEffManager.vulnerableMultiplier}% más de daño.";
    }
}
