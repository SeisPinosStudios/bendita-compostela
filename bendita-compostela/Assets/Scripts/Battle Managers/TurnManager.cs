using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    [field:SerializeField] public static TurnManager Instance { get; private set; }
    [SerializeField] Transform enemiesContainer;
    [field: SerializeField] public LinkedList<EntityBehaviour> turnQueue { get; private set; } = new LinkedList<EntityBehaviour>();
    [field: SerializeField] public EntityBehaviour entityTurn { get; private set; }
    [field: SerializeField] public List<EnemyBehaviour> enemiesBehaviour { get; private set; }
    [field: SerializeField] public PlayerBehaviour playerBehaviour { get; private set; }
    public event Action OnTurn = delegate { };

    private void Awake()
    {
        Instance = this;
        foreach (Transform enemy in enemiesContainer) enemiesBehaviour.Add(enemy.GetComponent<EnemyBehaviour>());

        turnQueue.AddLast(playerBehaviour);
        foreach (EnemyBehaviour enemy in enemiesBehaviour) turnQueue.AddLast(enemy);
    }
    private void Start()
    {
        print($"{turnQueue.First.Value}");
        Turn();
    }

    public void Turn()
    {
        if (entityTurn) turnQueue.AddLast(entityTurn);
        foreach (EntityBehaviour behaviour in turnQueue) print(behaviour.name);
        StartCoroutine(TurnCoroutine());
    }
    private IEnumerator TurnCoroutine()
    {
        yield return new WaitUntil(() => entityTurn == null || entityTurn.isTurn == false);

        //entityTurn = turnQueue.Dequeue();
        entityTurn = turnQueue.First.Value;
        turnQueue.RemoveFirst();
        entityTurn.OnTurnBegin();
        OnTurn();
    }
}
