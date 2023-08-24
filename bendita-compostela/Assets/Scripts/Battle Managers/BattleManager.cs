using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [field:SerializeField, Header("Battle Setup")] public List<EnemyData> enemyDataList { get; private set; }
    [SerializeField] EntityDataContainer entityDataContainer;

    [SerializeField, Header("Enviroment Variables")] Transform enemiesContainer;
    [field:SerializeField] public static BattleManager Instance { get; private set; }
    [field:SerializeField] public Player player { get; private set; }
    [field:SerializeField] public List<Enemy> enemies { get; private set; }

    private void Awake()
    {
        Instance = this;

        foreach (EnemyData enemy in enemyDataList) 
        {
            entityDataContainer.entityData = enemy;
            Instantiate(entityDataContainer, enemiesContainer);
        }

        foreach (Transform child in enemiesContainer) enemies.Add(child.GetComponent<Enemy>());
    }

    public List<Enemy> GetDamagedEnemies(float percentage)
    {
        var damagedEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies) if (enemy.IsDamaged(percentage)) damagedEnemies.Add(enemy);
        return damagedEnemies;
    }
}
