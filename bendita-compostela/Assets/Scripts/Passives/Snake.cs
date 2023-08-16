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
}
