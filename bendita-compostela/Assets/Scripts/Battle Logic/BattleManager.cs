using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [field:SerializeField] public static BattleManager Instance { get; private set; }
    [field: SerializeField] public Player player { get; private set; }
    [field: SerializeField] public List<Enemy> enemies { get; private set; }
    [SerializeField] Transform enemiesContainer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        foreach (Transform child in enemiesContainer) enemies.Add(child.GetComponent<Enemy>());
    }
    private void Start()
    {
        player.Turn();
    }
}
