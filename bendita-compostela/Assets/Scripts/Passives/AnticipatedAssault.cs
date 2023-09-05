using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnticipatedAssault : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(PassiveEffect());
    }

    private IEnumerator PassiveEffect()
    {
        yield return new WaitUntil(() => TurnManager.Instance);
        var enemyBehaviour = GetComponent<EnemyBehaviour>();
        TurnManager.Instance.turnQueue.Remove(enemyBehaviour);
        TurnManager.Instance.turnQueue.AddFirst(enemyBehaviour);
    }
}
