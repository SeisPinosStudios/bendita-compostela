using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BasicAlteredEffect;

public class Frenzy : BasicAlteredEffect
{
    public static void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        var card = (CardData)data;
        if (!entityEffectsManager.frenzyAttacks.ContainsKey(card)) entityEffectsManager.frenzyAttacks.Add(card, 1);
        else entityEffectsManager.frenzyAttacks[card] += 1;
    }
}
