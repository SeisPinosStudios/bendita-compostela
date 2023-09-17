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

    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=11> Frenes�: Al realizar un ataque mientras sufres <sprite=11>, ese ataque har� 1 punto m�s de da�o durante el resto del combate. " +
            $"El efecto es acumulable.";
    }
    public static string GetBasicDescription()
    {
        return Frenzy.GetDescription(null, null);
    }
}
