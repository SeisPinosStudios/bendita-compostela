using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    [SerializeField] Player player;
    public override void OnTurnBegin()
    {
        print($"{this.name} OnBeginTurn");
        StartCoroutine(OnTurnBeginCorroutine());
    }
    private IEnumerator OnTurnBeginCorroutine()
    {
        yield return StartCoroutine(DeckManager.Instance.DrawCardCoroutine(5));
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
        yield return StartCoroutine(DeckManager.Instance.ReturnCardsCoroutine());
        isTurn = !isTurn;
    }
}
