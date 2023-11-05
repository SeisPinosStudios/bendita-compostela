using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void Awake()
    {
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += EnableCond;
        Damage.OnAttack += DisableCond;
    }
    private void DisableCond(GameObject target, GameObject user, CardData card)
    {
        if (user.GetComponent<Entity>() is not Player) return;
        BattleManager.Instance.player.AttackMultiplier(-1.0f);
    }
    private void EnableCond()
    {
        BattleManager.Instance.player.AttackMultiplier(1.0f);
    }
    private void OnDestroy()
    {
        TurnManager.Instance.playerBehaviour.OnPlayerTurn -= EnableCond;
        Damage.OnAttack -= DisableCond;
    }
    public static void OnObtain()
    {
        
    }
}
