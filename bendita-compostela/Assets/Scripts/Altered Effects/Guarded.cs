using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guarded : MonoBehaviour
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=3> En Guardia: El pr�ximo ataque que reciba har� un {entityEffManager.guardedMultiplier*100}% menos de da�o.";
    }
}
