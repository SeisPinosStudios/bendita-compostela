using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanse : BasicCardEffect
{
    public static void Effect(string damage, CardData card, GameObject user, GameObject target)
    {
        var entityEffectsManager = user.GetComponent<EntityEffectsManager>();
        foreach (TAlteredEffects.AlteredEffects effect in TAlteredEffects.negativeEffects) entityEffectsManager.RemoveEffect(effect, 99);
    }
}
