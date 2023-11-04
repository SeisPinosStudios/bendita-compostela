using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : EntityBehaviour
{
    [SerializeField] Player player;
    public event Action OnPlayerTurn = delegate { };
    
    public override IEnumerator OnTurnBegin()
    {
        OnPlayerTurn();

        yield return base.StartCoroutine(base.OnTurnBegin());

        yield return StartCoroutine(DeckManager.Instance.DrawCardCoroutine(5));

        BattleManager.Instance.SetInteraction(true);

        player.RestoreEnergy(player.maxEnergy);

        isTurn = !isTurn;

        ChangeBehaviourState(BehaviourState.OnTurn);
    }
    public override IEnumerator OnTurn()
    {
        yield return base.StartCoroutine(base.OnTurn());
    }
    public override IEnumerator OnTurnEnd()
    {
        yield return base.StartCoroutine(base.OnTurnEnd());

        BattleManager.Instance.SetInteraction(false);
        
        yield return StartCoroutine(DeckManager.Instance.ReturnCardsCoroutine());

        TurnManager.Instance.Turn();
        isTurn = !isTurn;
    }
}
