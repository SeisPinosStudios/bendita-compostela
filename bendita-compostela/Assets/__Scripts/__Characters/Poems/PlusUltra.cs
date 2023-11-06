using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusUltra : PoemEffect
{
    public static new void Effect()
    {
        BattleManager.Instance.player.AddMaxEnergy(5);
    }
}
