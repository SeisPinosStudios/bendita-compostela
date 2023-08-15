using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Transform enemiesContainer;
    [field:SerializeField] public static BattleManager Instance { get; private set; }
    [field:SerializeField] public Player player { get; private set; }
    [field:SerializeField] public List<Enemy> enemies { get; private set; }

    private void Awake()
    {
        if (!Instance) Instance = this;
        foreach (Transform child in enemiesContainer) enemies.Add(child.GetComponent<Enemy>());
    }

    public List<Enemy> GetDamagedEnemies(float percentage)
    {
        var damagedEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies) if (enemy.IsDamaged(percentage)) damagedEnemies.Add(enemy);
        return damagedEnemies;
    }
}
