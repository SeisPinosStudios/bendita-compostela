using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarmed : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=7> Desarmado: No puedes ni cambiar ni equiparte armas.";
    }
}
