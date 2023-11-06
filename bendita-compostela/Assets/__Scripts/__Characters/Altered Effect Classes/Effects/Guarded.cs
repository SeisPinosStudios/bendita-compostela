using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guarded : MonoBehaviour
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=3> En Guardia: El próximo ataque que reciba hará un {entityEffManager.guardedMultiplier*100}% menos de daño.";
    }
    public static string GetBasicDescription()
    {
        return "<sprite=3> En Guardia: bloquea parte del daño del siguiente ataque";
    }
}
