using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void Awake()
    {
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += DisableCond;
        BattleManager.Instance.player.AttackMultiplier(1.0f);
    }
    private void DisableCond()
    {
        BattleManager.Instance.player.AttackMultiplier(-1.0f);
    }
    public static void OnObtain()
    {
        return;
    }
}
