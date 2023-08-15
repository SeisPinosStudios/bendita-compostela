using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : EntityBehaviour
{
    [SerializeField, Header("Enemy Behaviour variables")] EnemyData enemyData;
    [SerializeField] CardDataContainer attackPrefab;
    [SerializeField] Transform mainCanvas;
    [SerializeField] float waitTime;
    [field: SerializeField] public Queue<EnemyAttack> attackQueue { get; private set; } = new Queue<EnemyAttack>();
    [field: SerializeField] public EnemyAttackSequence currentSequence { get; private set; }

    private void Awake()
    {
        enemyData = (EnemyData)entityDataContainer.entityData;
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }
    public override void OnTurnBegin()
    {
        print($"{this.name} OnBeginTurn");
        if (attackQueue.Count <= 0) GetNewSequence();
        isTurn = !isTurn;
        OnTurn();
    }
    public override void OnTurn()
    {
        StartCoroutine(OnTurnCoroutine());
    }
    private IEnumerator OnTurnCoroutine()
    {
        yield return StartCoroutine(Attack());
        OnTurnEnd();
    }
    public override void OnTurnEnd()
    {
        isTurn = !isTurn;
        TurnManager.Instance.Turn();
    }


    private IEnumerator Attack()
    {
        var attack = attackQueue.Dequeue();
        var target = GetTarget(attack);
        attackPrefab.cardData = attack;

        var attackInstance = Instantiate(attackPrefab, mainCanvas);
        attackInstance.GetComponent<Card>().UseCard(target);
        yield return new WaitForSeconds(waitTime);
        Destroy(attackInstance.gameObject);
    }
    private void GetNewSequence()
    {
        currentSequence = ChooseSequence();
        foreach (EnemyAttack attack in currentSequence.attacks) attackQueue.Enqueue(attack);
    }
    private EnemyAttackSequence ChooseSequence()
    {
        if (enemyData.dyingSequence != null && entity.IsDamaged(0.3f)) return enemyData.dyingSequence;

        if (enemyData.healSequence != null && BattleManager.Instance.GetDamagedEnemies(0.3f).Count > 0) return enemyData.healSequence;
        
        if (enemyData.attackSequence != null && entity.IsDamaged(0.5f)) return enemyData.attackSequence;

        return enemyData.defaultSequence;
    }
    private GameObject GetTarget(EnemyAttack attack)
    {
        if (attack.attackType == EnemyAttack.AttackType.Attack) return BattleManager.Instance.player.gameObject;

        return null;
    }
}
