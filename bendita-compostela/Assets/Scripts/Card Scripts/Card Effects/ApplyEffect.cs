using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class ApplyEffect : BasicCardEffect
{

    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var effectData = data.Split("|");
        Enum.TryParse(effectData[0], out TAlteredEffects.AlteredEffects effect);
        var value = effectData[1];
        target.GetComponent<EntityEffectsManager>().ApplyEffect(effect, int.Parse(value));
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        var description = new StringBuilder();
        description.Append("Aplica");
        foreach(string data in card.GetAlteredEffects())
        {
            var splitedData = data.Split("|");
            Enum.TryParse(splitedData[0], out TAlteredEffects.AlteredEffects effect);
            var value = splitedData[1];
            description.Append($" {value}<sprite={(int)effect}>");
        }
        description.Append(".");
        return description.ToString();
    }
}
