using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    #region Variables
    public static MapManager Instance;
    private Grid mapGrid;

    [Header("Map SetUp")]
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject mapSpace; 
    [SerializeField] private GameObject lineRendererPrefab;
    
    [Header("Map Configuration")]
    [SerializeField] private int NUM_NODES;
    [SerializeField] private int MAP_DEPTH;
    [SerializeField] private int NUM_PATHS_GENERATED;     
    [SerializeField] private List<CombatPool> combatPools = new List<CombatPool>();    
    [SerializeField] private CombatPool bossPool;
    public EventOdds[] encounterProbabilities;
    public List<GameObject> encounterPrefabs;

    [Header("Map Node Display")]
    [SerializeField] private GameObject bossPrefab;    
    [SerializeField] private GameObject combatPrefab;    
    [SerializeField] private GameObject eventPrefab;    
    [SerializeField] private GameObject shopPrefab;   

    public Stack<GameObject> eventPrefabsStack;    
    private Dictionary<NodeEncounter, GameObject> encounterPrefabsDictionary = new Dictionary<NodeEncounter, GameObject>();

    public Vector2 MAP_DISPLAY_OFFSET = new Vector2(1,4);    

    [Header("Player Selections")]
    public List<Node> nodesVisited = new List<Node>();
    
    // Dictionary with the current nodes GO given their position
    private Dictionary<Vector2,GameObject> nodeGameObjects = new Dictionary<Vector2, GameObject>();
    #endregion
    
    #region Initialization and setup of the Singleton
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }       
    }
    
    private void Start() 
    { 
        encounterPrefabsDictionary.Add(NodeEncounter.CombatEncounter, combatPrefab);
        encounterPrefabsDictionary.Add(NodeEncounter.EventEncounter, eventPrefab);
        encounterPrefabsDictionary.Add(NodeEncounter.ShopEncounter, shopPrefab);        
        //ConfigureMap();
    }
    #endregion    
        
    #region Public Methods

    /// <summary>
    /// Creates and displays a new map each time is called, erasing the old map information and display
    /// </summary>
    public void ConfigureMap()
    {
        RandomizeEvents();
        mapGrid = new Grid(NUM_NODES, MAP_DEPTH, NUM_PATHS_GENERATED);
        mapGrid.Boss.NodeEncounter = NodeEncounter.CombatEncounter;
        SetBoss(0);
        //line.positionCount = mapGrid.Nodes.Count;        
        foreach (Transform son in mapSpace.transform)
        {
            Destroy(son.gameObject);
        }
        nodeGameObjects.Clear();
        DisplayMap();
        EneableFirstNodes();
    }

    public List<CombatPool> GetCombatPools()
    {
        return combatPools;
    }
    
    public Grid GetCurrentGrid()
    {
        return mapGrid;
    }
    /// <summary>
    /// Sets the boss from a boss pool given an index, but only in the grid information, not in the map displayed
    /// </summary>
    /// <param name="bossIdx">index of the boss</param>
    public void SetBoss(int bossIdx)
    {
        mapGrid.Boss.CombatData = bossPool.combatsData[bossIdx];
    }
    /// <summary>
    /// Asigns the boss information to the boss node displayed in the map
    /// </summary>
    public void AsignBossEncounter()
    {
        nodeGameObjects[mapGrid.Boss.NodePos].GetComponent<NodeEvent>().nodeInfo = mapGrid.Boss;
    }
    /// <summary>
    /// Loads and displays a map given the grid information and the progress of the map
    /// </summary>
    /// <param name="map"> Map to load</param>
    /// <param name="currentProgression"> Player progression node list </param>
    public void LoadMap(Grid map, List<Node> currentProgression)
    {
        mapGrid = map;
        DisplayMap();
        foreach (Node node in currentProgression)
        {
            nodeGameObjects[node.NodePos].GetComponent<NodeEvent>().isCompleted = true;
        }
        EneableNextAvailableNodes(currentProgression[currentProgression.Count-1]);
    }
    /// <summary>
    /// Map display change when a given node is selected
    /// </summary>
    /// <param name="nodeSelected"></param>
    public void NodeSelected(Node nodeSelected)
    {
        nodesVisited.Add(nodeSelected);
        DisableNotSelectedNodes(nodeSelected);
        EneableNextAvailableNodes(nodeSelected);
    }
    #endregion

    #region Private Methods
    private void DisplayMap()
    {        
        //TODO make more efficient

        // Instantiate Nodes
        foreach (Node node in mapGrid.Nodes.Values)
        {
            if(node.futureNodes.Count != 0)
                    {     
                        //Canvas version         
                        //var nodePosition = new Vector2((x*85)+900, (y*85)+200);
                        
                        //Normal version
                        var nodePosition = new Vector2(node.NodePos.x - MAP_DISPLAY_OFFSET.x, node.NodePos.y - MAP_DISPLAY_OFFSET.y);

                        var spawnedNode = Instantiate(encounterPrefabsDictionary[node.NodeEncounter], nodePosition, Quaternion.identity, mapSpace.transform);                        
                        
                        spawnedNode.GetComponent<NodeEvent>().nodeInfo = node;

                        nodeGameObjects.Add(node.NodePos, spawnedNode);
                        
                    }
        }
        var bossNode = Instantiate(bossPrefab, new Vector2(mapGrid.Boss.NodePos.x - MAP_DISPLAY_OFFSET.x, mapGrid.Boss.NodePos.y - MAP_DISPLAY_OFFSET.y), Quaternion.identity, mapSpace.transform);
        bossNode.GetComponent<NodeEvent>().nodeInfo = mapGrid.Boss;
        nodeGameObjects.Add(mapGrid.Boss.NodePos, bossNode);

        // Instantiate Paths
        foreach (List<Node> path in mapGrid.Paths)
        {            
            var line = Instantiate(lineRendererPrefab, new Vector2(0,0), Quaternion.identity,mapSpace.transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.positionCount = mapGrid.Height+1;      
            lineRenderer.sortingOrder = 1;
            lineRenderer.widthMultiplier = 0.1f;            
            for (int i = 0; i < path.Count; i++)
            {
                    lineRenderer.SetPosition(i,new Vector2(path[i].NodePos.x - MAP_DISPLAY_OFFSET.x, path[i].NodePos.y - MAP_DISPLAY_OFFSET.y));                
            }
            lineRenderer.SetPosition(path.Count, bossNode.transform.position);            
        } 
    }
    private void EneableFirstNodes()
    {
        foreach (Vector2 key in nodeGameObjects.Keys)
        {
            if(key.y == 0)nodeGameObjects[key].GetComponent<Collider2D>().enabled = true;            
        }
    }
    
    private void EneableNextAvailableNodes(Node nodeSelected)
    {
        foreach (Node node in nodeSelected.futureNodes)
        {
            nodeGameObjects[node.NodePos].GetComponent<Collider2D>().enabled = true;
        }
    }
    private void DisableNotSelectedNodes(Node nodeSelected)
    {
        foreach (Vector2 key in nodeGameObjects.Keys)
        {
            if(key.y == nodeSelected.NodePos.y)
            {
                nodeGameObjects[key].GetComponent<Collider2D>().enabled = false;                
            }            
            nodeGameObjects[nodeSelected.NodePos].GetComponent<NodeEvent>().isCompleted = true;
        }
        
    }
    private void RandomizeEvents()
    {           
        encounterPrefabs = encounterPrefabs.OrderBy( x => Random.Range(0,10)).ToList();
       
        eventPrefabsStack = new Stack<GameObject>(encounterPrefabs);        
    }
    #endregion

    #region Debug
    public void TotalNodesIF(string a)
    {
        NUM_NODES = int.Parse(a);
    }
    public void MapDepthIF(string b)
    {
        MAP_DEPTH = int.Parse(b);
    }
    public void NumPathsGeneratedIF(string c)
    {
        NUM_PATHS_GENERATED = int.Parse(c);
    } 
    #endregion
}

