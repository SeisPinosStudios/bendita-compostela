using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private Grid mapGrid;

    [Header("Map SetUp")]
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject mapSpace; 
    [SerializeField] private GameObject lineRendererPrefab;
    
    [Header("Map Configuration")]
    [SerializeField] private int numNodes;
    [SerializeField] private int mapDepth;
    [SerializeField] private int numPathsGenerated;     
    [SerializeField] private List<CombatPool> combatPools = new List<CombatPool>();    
    [SerializeField] private CombatPool bossPool;
    [SerializeField] private EventOdds[] encounterProbabilities;
    [SerializeField] private List<GameObject> encounterPrefabs;

    [Header("Map Node Display")]
    [SerializeField] private GameObject bossPrefab;    
    [SerializeField] private GameObject combatPrefab;    
    [SerializeField] private GameObject eventPrefab;    
    [SerializeField] private GameObject shopPrefab;   

    private Stack<GameObject> eventPrefabsStack;    
    private Dictionary<NodeEncounter, GameObject> encounterPrefabsDictionary = new Dictionary<NodeEncounter, GameObject>();

    public Vector2 MAP_DISPLAY_OFFSET = new Vector2(1,4);
    private Color[] colors = {Color.yellow, Color.blue, Color.green, Color.white, Color.black};

    [Header("Player Selections")]
    public List<Node> nodesVisited = new List<Node>();

    private Dictionary<Vector2,GameObject> nodeGameObjects = new Dictionary<Vector2, GameObject>();

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
        ConfigureMap();      
    }

    public void ConfigureMap()
    {
        RandomizeEvents();
        mapGrid = new Grid(numNodes,mapDepth,numPathsGenerated,encounterProbabilities,eventPrefabsStack);
        mapGrid.Boss.NodeEncounter = NodeEncounter.CombatEncounter;
        mapGrid.Boss.CombatData = bossPool.combatsData[Random.Range(0,bossPool.combatsData.Count)];
        //line.positionCount = mapGrid.Nodes.Count;        
        foreach (Transform son in mapSpace.transform)
        {
            Destroy(son.gameObject);
        }
        nodeGameObjects.Clear();
        DisplayMap();
        EneableFirstNodes();
    }

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
        //int colorIdx = 0;
        foreach (List<Node> path in mapGrid.Paths)
        {            
            var line = Instantiate(lineRendererPrefab, new Vector2(0,0), Quaternion.identity,mapSpace.transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.positionCount = mapGrid.Height+1;      
            lineRenderer.sortingOrder = 1;
            /*lineRenderer.material = new Material(Shader.Find("Sprites/Default"))
            {                
                color = colors[colorIdx]
            };*/
            lineRenderer.widthMultiplier = 0.1f;            
            for (int i = 0; i < path.Count; i++)
            {
                    lineRenderer.SetPosition(i,new Vector2(path[i].NodePos.x - MAP_DISPLAY_OFFSET.x, path[i].NodePos.y - MAP_DISPLAY_OFFSET.y));                
            }
            lineRenderer.SetPosition(path.Count, bossNode.transform.position);
            //colorIdx++;
        } 
    }
    private void EneableFirstNodes()
    {
        foreach (Vector2 key in nodeGameObjects.Keys)
        {
            if(key.y == 0)nodeGameObjects[key].GetComponent<Collider2D>().enabled = true;            
        }
    }
    public void NodeSelected(Node nodeSelected)
    {
        nodesVisited.Add(nodeSelected);
        DisableNotSelectedNodes(nodeSelected);        
        EneableNextAvailableNodes(nodeSelected);
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
            if(key.y == nodeSelected.NodePos.y)nodeGameObjects[key].GetComponent<Collider2D>().enabled = false;
        }
    }
    public List<CombatPool> GetCombatPools()
    {
        return combatPools;
    }
    public void RandomizeEvents()
    {   
        encounterPrefabs = encounterPrefabs.OrderBy( x => Random.Range(0,10)).ToList();
        eventPrefabsStack = new Stack<GameObject>(encounterPrefabs);
    }


    #region Debug

    public void TotalNodesIF(string a)
    {
        numNodes = int.Parse(a);
    }
    public void MapDepthIF(string b)
    {
        mapDepth = int.Parse(b);
    }
    public void NumPathsGeneratedIF(string c)
    {
        numPathsGenerated = int.Parse(c);
    } 

    #endregion
}

