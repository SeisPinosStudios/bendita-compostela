using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField, Header("Player Data")] private PlayerData playerDataPreset;
    [field:SerializeField] public PlayerData playerData { get; private set; }
    [field:SerializeField] public CombatData combatData { get; private set; }
    [field: SerializeField, Header("Map State and Progression")] public Grid map { get; private set; }
    [field: SerializeField] public List<Node> visitedNodes { get; private set; }

    private void Awake()
    {
        if(!Instance) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(this.gameObject);
        
        playerData = playerDataPreset.Copy();
    }

    #region Combat
    public void SetCombat(CombatData combatData)
    {
        this.combatData = combatData;
    }
    #endregion
    
    #region Map and Progress
    public void AddVisitedNode(Node node)
    {
        visitedNodes.Add(node);
    }
    public void SaveGrid(Grid grid)
    {
        map = grid;
    }
    #endregion
}
