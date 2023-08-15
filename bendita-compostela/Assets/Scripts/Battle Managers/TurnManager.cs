using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    [field:SerializeField] public static TurnManager Instance { get; private set; }
    [SerializeField] Transform enemiesContainer;
    [field: SerializeField] public Queue<EntityBehaviour> turnQueue { get; private set; } = new Queue<EntityBehaviour>();
    [field: SerializeField] public EntityBehaviour entityTurn { get; private set; }
    [field: SerializeField] public List<EnemyBehaviour> enemiesBehaviour { get; private set; }
    [field: SerializeField] public PlayerBehaviour playerBehaviour { get; private set; }
    public event Action onTurn;

    private void Awake()
    {
        if (!Instance) Instance = this;
        foreach (Transform enemy in enemiesContainer) enemiesBehaviour.Add(enemy.GetComponent<EnemyBehaviour>());

        turnQueue.Enqueue(playerBehaviour);
        foreach (EnemyBehaviour enemy in enemiesBehaviour) turnQueue.Enqueue(enemy);
    }

    private void Start()
    {
        Turn();
    }

    public void Turn()
    {
        if (entityTurn) turnQueue.Enqueue(entityTurn);
        StartCoroutine(TurnCoroutine());
    }
    private IEnumerator TurnCoroutine()
    {
        yield return new WaitUntil(() => entityTurn == null || entityTurn.isTurn == false);
        entityTurn = turnQueue.Dequeue();
        entityTurn.OnTurnBegin();
        onTurn();
    }
}
