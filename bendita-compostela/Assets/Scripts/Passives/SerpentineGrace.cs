using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentineGrace : BasicPassive
{
    [field: SerializeField] public bool active { get; private set; }
    private void Awake()
    {
        GetComponent<EnemyBehaviour>().OnEnemyTurn += PassiveEffect;
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += ResetPassive;
    }

    public void PassiveEffect()
    {
        if (!active) return;
        if (BattleManager.Instance.player.entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Poison))
            TurnManager.Instance.turnQueue.AddFirst(GetComponent<EnemyBehaviour>());

        active = false;
    }

    private void ResetPassive()
    {
        active = true;
    }
}
