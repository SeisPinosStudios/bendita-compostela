using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private EventOdds[] eventsProbabilities;
    
    private Vector2 MAP_DISPLAY_OFFSET = new Vector2(1,4);
    private Color[] colors = {Color.yellow, Color.blue, Color.green, Color.white, Color.black};

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
        ConfigureMap();      

    }

    public void ConfigureMap()
    {
        mapGrid = new Grid(numNodes,mapDepth,numPathsGenerated,eventsProbabilities);
        mapGrid.Boss.NodeEvent = bossPrefab;
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
        //int lineCount = 0;
        // Instantiate Nodes
        foreach (Node node in mapGrid.Nodes.Values)
        {
            if(node.futureNodes.Count != 0)
                    {     
                        //Canvas version         
                        //var nodePosition = new Vector2((x*85)+900, (y*85)+200);
                        
                        //Normal version
                        var nodePosition = new Vector2(node.NodePos.x - MAP_DISPLAY_OFFSET.x, node.NodePos.y - MAP_DISPLAY_OFFSET.y);

                        var spawnedNode = Instantiate(node.NodeEvent, nodePosition, Quaternion.identity, mapSpace.transform);                        
                        
                        spawnedNode.GetComponent<NodeEvent>().nodeInfo = node;

                        nodeGameObjects.Add(node.NodePos, spawnedNode);
                        
                        
                        /*line.SetPosition(lineCount, nodePosition);
                        lineCount++;*/
                    }
        }
        var bossNode = Instantiate(mapGrid.Boss.NodeEvent, new Vector2(mapGrid.Boss.NodePos.x - MAP_DISPLAY_OFFSET.x, mapGrid.Boss.NodePos.y - MAP_DISPLAY_OFFSET.y), Quaternion.identity, mapSpace.transform);
        bossNode.GetComponent<NodeEvent>().nodeInfo = mapGrid.Boss;
        nodeGameObjects.Add(mapGrid.Boss.NodePos, bossNode);
        // Instantiate Paths
        int colorIdx = 0;
        foreach (List<Node> path in mapGrid.Paths)
        {            
            var line = Instantiate(lineRendererPrefab, new Vector2(0,0), Quaternion.identity,mapSpace.transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.positionCount = mapGrid.Height+1;      
            lineRenderer.sortingOrder = 1;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"))
            {                
                color = colors[colorIdx]
            };
            lineRenderer.widthMultiplier = 0.1f;
            Debug.Log("Path.Count: " + path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                    lineRenderer.SetPosition(i,new Vector2(path[i].NodePos.x - MAP_DISPLAY_OFFSET.x, path[i].NodePos.y - MAP_DISPLAY_OFFSET.y));                
            }
            lineRenderer.SetPosition(path.Count, bossNode.transform.position);
            colorIdx++;
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

