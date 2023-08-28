using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PoemEffect
{
    public static new void Effect()
    {
        var player = BattleManager.Instance.player;
        player.RestoreHealth(8, player.healingBonus, player.healingMultiplier);
    }
}
