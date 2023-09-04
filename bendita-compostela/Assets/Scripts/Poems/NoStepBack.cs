using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStepBack : PoemEffect
{
    public static new void Effect()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.AttackMultiplier(-0.25f);
    }
}
