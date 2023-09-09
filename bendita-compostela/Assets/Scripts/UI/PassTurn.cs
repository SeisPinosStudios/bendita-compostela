using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    [SerializeField] private Animator passTurnAnimator;
    private void Start()
    {
        TurnManager.Instance.OnTurn += StartTurn;
    }
    private void OnDestroy()
    {
        TurnManager.Instance.OnTurn -= StartTurn;
    }
    public void EndTurn() 
    {
        passTurnAnimator.SetBool("Clicked", true);
    }
    public void StartTurn() 
    {
        if(TurnManager.Instance.entityTurn.entity == BattleManager.Instance.player) 
            passTurnAnimator.SetBool("Clicked", false);
    }
}
