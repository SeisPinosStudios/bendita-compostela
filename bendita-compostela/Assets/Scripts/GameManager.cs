using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField, Header("Player Data")] private PlayerData playerDataPreset;
    [field:SerializeField] public PlayerData playerData { get; private set; }
    [field:SerializeField] public CombatData combatData { get; private set; }
    [field: SerializeField, Header("Map State and Progression")] public Grid map { get; private set; }
    [field: SerializeField] public List<Node> visitedNodes { get; private set; }
    [field: SerializeField, Header("Debug")] public bool debug { get; private set; }


    private void Awake()
    {
        if(!Instance) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(this.gameObject);
        
        playerData = playerDataPreset.Copy();

        if (debug)
        {
            foreach (CardData objectCard in SODataBase.objects) playerData.inventory.Add(objectCard.Copy());
            foreach (CardData special in SODataBase.special) playerData.inventory.Add(special.Copy());
            foreach (WeaponData weapon in SODataBase.weapons) playerData.inventory.Add(weapon.Copy());
            foreach (ArmorData armor in SODataBase.armors) playerData.inventory.Add(armor.Copy());
            playerData.AddCoins(100);
        }
        else
        {
            playerData.deck.Add(SODataBase.weapons[Random.Range(0, SODataBase.weapons.Count)]);
            for(int i = 0; i < 6; i++) playerData.deck.Add(SODataBase.objects[Random.Range(0, SODataBase.objects.Count)]);
            playerData.chestArmor = SODataBase.chestArmors[Random.Range(0, SODataBase.chestArmors.Count)].Copy();
            playerData.legArmor = SODataBase.legArmors[Random.Range(0, SODataBase.legArmors.Count)].Copy();
        }
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.)) 
        {
        
        }
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

    #region Useful Methods
    public static void DestoyObject(GameObject gameObject)
    {
        DestoyObject(gameObject);
    }
    #endregion
}
