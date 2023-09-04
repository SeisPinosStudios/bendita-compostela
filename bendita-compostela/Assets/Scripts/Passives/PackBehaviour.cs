using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackBehaviour : BasicPassive
{
    [field:SerializeField] public Enemy enemy { get; private set; }
    [field:SerializeField] public List<Enemy> enemies { get; private set; }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemies = BattleManager.Instance.enemies;
        enemy.DefenseBonus(2);
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDeath += CheckAllies;
            enemy.GetComponent<EntityEffectsManager>().GuardedMultiplier(0.25f);
        }
    }

    private void CheckAllies()
    {
        if (BattleManager.Instance.enemies.Count >= 1) return;
        enemy.DefenseBonus(-2);
        enemy.GetComponent<EntityEffectsManager>().GuardedMultiplier(-0.25f);
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in enemies) enemy.OnDeath -= CheckAllies;
    }
}
