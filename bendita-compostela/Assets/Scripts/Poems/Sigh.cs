using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigh : PoemEffect
{
    public static new void Effect()
    {
        BattleManager.Instance.player.AttackMultiplier(1.0f);
    }
}
