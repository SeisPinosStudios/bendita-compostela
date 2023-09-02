using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ApplyEffectAll : BasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var effectData = data.Split("|");
        Enum.TryParse(effectData[0], out TAlteredEffects.AlteredEffects effect);
        var value = effectData[1];
        foreach(Enemy enemy in BattleManager.Instance.enemies) enemy.entityEffectsManager.ApplyEffect(effect, int.Parse(value));
    }
}
