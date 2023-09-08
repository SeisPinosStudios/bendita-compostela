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

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return $"Limpia todo los efectos de estado.";
    }
}
