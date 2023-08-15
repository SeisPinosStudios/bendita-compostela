using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreEnergy : BasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        BattleManager.Instance.player.RestoreEnergy(int.Parse(data));
    }
}
