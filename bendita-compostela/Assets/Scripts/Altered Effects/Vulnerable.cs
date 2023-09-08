using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=2> Vulnerable: El pr�ximo ataque que reciba har� un {entityEffManager.vulnerableMultiplier*100}% m�s de da�o.";
    }
    public static string GetBasicDescription()
    {
        return $"<sprite=2> Vulnerable: los personajes que lo sufran recibir�n m�s da�o por ataques";
    }
}
