using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samael : CondecorationEffect
{
    private void Awake()
    {
        BattleManager.Instance.player.AttackMultiplier(0.5f);
        BattleManager.Instance.player.DefenseMultiplier(-0.5f);
    }
}
