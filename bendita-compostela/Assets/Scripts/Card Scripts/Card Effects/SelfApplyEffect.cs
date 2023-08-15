using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelfApplyEffect : BasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var effectData = data.Split("|");
        Enum.TryParse(effectData[0], out TAlteredEffects.AlteredEffects effect);
        var value = effectData[1];
        user.GetComponent<EntityEffectsManager>().ApplyEffect(effect, int.Parse(value));
    }
}
