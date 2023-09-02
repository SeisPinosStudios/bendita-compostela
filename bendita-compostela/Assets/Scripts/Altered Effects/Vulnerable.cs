using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=2> Vulnerable: El pr�ximo ataque que reciba har� un {entityEffManager.vulnerableMultiplier}% m�s de da�o.";
    }
}
