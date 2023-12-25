using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreEnergy : IBasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        BattleManager.Instance.player.RestoreEnergy(int.Parse(data));
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        var data = card.GetEffect(CardData.Effect.RestoreEnergy);
        return $"Restaura {data[0]} puntos de energ�a.";
    }
}
