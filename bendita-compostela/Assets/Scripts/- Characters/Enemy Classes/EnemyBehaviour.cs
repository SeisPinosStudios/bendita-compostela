using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBehaviour : EntityBehaviour
{
    [SerializeField, Header("Enemy Behaviour variables")] EnemyData enemyData;
    [SerializeField] CardDataContainer attackPrefab;
    [SerializeField] Transform mainCanvas;
    [SerializeField] float waitTime;
    [field: SerializeField] public Queue<EnemyAttack> attackQueue { get; private set; } = new Queue<EnemyAttack>();
    [field: SerializeField] public EnemyAttackSequence currentSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence.SequenceType sequenceType { get; private set; }
    public event Action OnEnemyTurn = delegate { };

    private void Awake()
    {
        enemyData = (EnemyData)entityDataContainer.entityData;
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }
    public override IEnumerator OnTurnBegin()
    {
        yield return base.StartCoroutine(base.OnTurnBegin());

        if (attackQueue.Count <= 0) GetNewSequence();

        isTurn = !isTurn;

        ChangeBehaviourState(BehaviourState.OnTurn);
    }
    public override IEnumerator OnTurn()
    {
        OnEnemyTurn();

        yield return base.StartCoroutine(base.OnTurn());

        yield return StartCoroutine(Attack());

        yield return new WaitForSeconds(0.25f);

        ChangeBehaviourState(BehaviourState.OnTurnEnd);
    }
    public override IEnumerator OnTurnEnd()
    {
        yield return base.StartCoroutine(base.OnTurnEnd());

        isTurn = !isTurn;
        TurnManager.Instance.Turn();
    }

    private IEnumerator Attack()
    {
        var attack = attackQueue.Dequeue();
        var target = GetTarget(attack);
        attackPrefab.cardData = attack;
        attackPrefab.GetComponent<CardDisplay>().target = target.GetComponent<Entity>();

        var attackInstance = Instantiate(attackPrefab, CanvasUtils.GetMainCanvas().transform);
        attackInstance.GetComponent<Card>().UseEnemyCard(target);
        attackInstance.GetComponent<CardDisplay>().target = target.GetComponent<Entity>();
        yield return new WaitForSeconds(waitTime);
        //Destroy(attackInstance.gameObject);
    }
    private void GetNewSequence()
    {
        currentSequence = ChooseSequence();
        foreach (EnemyAttack attack in currentSequence.attacks) attackQueue.Enqueue(attack);
    }
    private EnemyAttackSequence ChooseSequence()
    {
        if (enemyData.dyingSequence != null && entity.IsDamaged(0.3f)) 
        { 
            sequenceType = EnemyAttackSequence.SequenceType.dyingSequence; 
            return enemyData.dyingSequence; 
        }

        if (enemyData.healSequence != null && BattleManager.Instance.GetDamagedEnemies(0.3f).Count > 0)
        {
            sequenceType = EnemyAttackSequence.SequenceType.healSequence;
            return enemyData.healSequence;
        }

        if (enemyData.attackSequence != null && entity.IsDamaged(0.5f))
        {
            sequenceType = EnemyAttackSequence.SequenceType.attackSequence;
            return enemyData.attackSequence;
        }

        sequenceType = EnemyAttackSequence.SequenceType.defaultSequence;
        return enemyData.defaultSequence;
    }
    private GameObject GetTarget(EnemyAttack attack)
    {
        if (attack.attackType == EnemyAttack.AttackType.Attack) return BattleManager.Instance.player.gameObject;

        return null;
    }
    public void Death()
    {
        isTurn = false;
        StopAllCoroutines();
        if(TurnManager.Instance.entityTurn == this) TurnManager.Instance.Turn();
    }
}
