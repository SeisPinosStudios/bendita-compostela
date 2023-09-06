using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : BasicPassive
{
    private void Awake()
    {
        var entityEffectsManager = gameObject.GetComponent<EntityEffectsManager>();
        entityEffectsManager.AddResistance(TAlteredEffects.AlteredEffects.Poison);
    }

    #region Description
    public static string GetDescription()
    {
        return $"Aguante de Sierpe: inmune a Veneno <sprite=1>";
    }
    #endregion
}
