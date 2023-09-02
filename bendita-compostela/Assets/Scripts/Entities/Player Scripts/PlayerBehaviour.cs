using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : EntityBehaviour
{
    [SerializeField] Player player;
    public event Action OnPlayerTurn = delegate { };
    public override void OnTurnBegin()
    {
        print($"{this.name} OnBeginTurn");
        StartCoroutine(OnTurnBeginCorroutine());
    }
    private IEnumerator OnTurnBeginCorroutine()
    {
        OnPlayerTurn();

        entityEffManager.Poison();

        if (entityEffManager.Suffering(TAlteredEffects.AlteredEffects.Stun))
        {
            OnTurnEnd();
            yield break;
        }

        BattleManager.Instance.SetInteraction(true);
        yield return StartCoroutine(DeckManager.Instance.DrawCardCoroutine(5));
        player.RestoreEnergy(player.maxEnergy);
        isTurn = !isTurn;
    }
    public override void OnTurn()
    {

    }

    public override void OnTurnEnd()
    {
        StartCoroutine(OnTurnEndCoroutine());
        TurnManager.Instance.Turn();
    }
    private IEnumerator OnTurnEndCoroutine()
    {
        entityEffManager.Burn();

        BattleManager.Instance.SetInteraction(false);
        yield return StartCoroutine(DeckManager.Instance.ReturnCardsCoroutine());
        isTurn = !isTurn;
    }
}
