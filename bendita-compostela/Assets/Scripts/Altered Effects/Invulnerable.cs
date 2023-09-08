using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffectsManager, Entity entity)
    {
        return $"<sprite=4> Invulnerable: Bloquea completamente el daño de ataques y efectos de estado.";
    }
    public static string GetBasicDescription()
    {
        return Invulnerable.GetDescription(null, null);
    }
}
