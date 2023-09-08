using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Lead: BasicAlteredEffect
{
    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return "<sprite=9> Plumbosis: El siguiente ataque que haga sólo realizará el 50% de daño.";
    }
    public static string GetBasicDescription()
    {
        return GetDescription(null, null);
    }
}
