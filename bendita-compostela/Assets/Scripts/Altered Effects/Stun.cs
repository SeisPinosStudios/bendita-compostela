using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=10> Aturdido: Pierde el pr�ximo turno.";
    }
}
