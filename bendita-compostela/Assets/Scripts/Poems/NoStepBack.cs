using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStepBack : PoemEffect
{
    public static new void Effect()
    {
        foreach (Enemy enemy in BattleManager.Instance.enemies) enemy.DamageMultiplier(-0.25f);
    }
}
