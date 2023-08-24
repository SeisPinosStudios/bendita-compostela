using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grid{    

    #region Grid Variables

    private int width;    
    private int numNodes;
    private int height;    
    private int numPathsGenerated;  
    private EventOdds[] eventOdds;
    private Dictionary<Vector2,Node> nodeDictionary = new Dictionary<Vector2,Node>();
    private Node bossNode;    

    private List<List<Node>> pathList = new List<List<Node>>();    
    
    #endregion

    #region Grid Initialization
    public Grid(int numNodes, int height, int numPathsGenerated, EventOdds[] eventOdds)
    {
        this.numNodes = numNodes;
        this.height = height;
        width = numNodes / height;
        this.numPathsGenerated = numPathsGenerated;     
        this.eventOdds = eventOdds;           
        GenerateNodes();
        
        for (int i = 0; i < numPathsGenerated; i++)
        {
            GeneratePath();    
        }

        GenerateBossNode();
        DeletePathlessNodes();        
        AsignNodesValues();
        AsignRandomEncounter();
        ShowAllNodes();
    }
    #endregion

    #region Grid Private Methods 
    private void GenerateBossNode()
    {
        bossNode = new Node(width/2,height);        
        foreach (Vector2 key in nodeDictionary.Keys)
        {
            //TODO remake more efficiently
            if(key.y == height-1 )
            {
                if(nodeDictionary.TryGetValue(key, out Node lastNodeInPath))
                {                                        
                    //Debug.Log($"{lastNodeInPath.NodePos}||{lastNodeInPath.IsPathless}");
                    if(!lastNodeInPath.IsPathless) lastNodeInPath.futureNodes.Add(bossNode);                    
                }
            }
        }
        nodeDictionary.Add(bossNode.NodePos, bossNode);
    }

    private void AsignNodesValues()
    {
        //Asign first layer nodes always a combat event
        foreach (Vector2 key in nodeDictionary.Keys)
        {
            if(key.y == 0)
            {
                nodeDictionary[key].NodeEncounter = NodeEncounter.CombatEncounter;                
            }            
        }
        //Asign all nodes value based on the probability fields above
        foreach (Node node in nodeDictionary.Values)
        {
            if(node.NodeEncounter == NodeEncounter.None)
            {
                //TODO remake more efficiently
                int i = Random.Range(0,100);
                for (int j = 0; j < eventOdds.Length; j++)
                {
                    if(i >= eventOdds[j].minProbabilityRange && i<= eventOdds[j].maxProbabilityRange)
                    {
                        node.NodeEncounter = eventOdds[j].nodeEncounter;                        
                    }
                }
            }
        }
    }

    private void AsignRandomEncounter()
    {        
        //TODO remake more efficiently
        var combatPoolsAux = MapManager.Instance.GetCombatPools();
        List<Node> combatNodeList = new List<Node>();
        List<Node> eventNodeList = new List<Node>();
        foreach (Node node in nodeDictionary.Values)
        {
            switch (node.NodeEncounter)
            {                
                case NodeEncounter.CombatEncounter:
                    combatNodeList.Add(node);
                    break;
                case NodeEncounter.EventEncounter:
                    eventNodeList.Add(node);
                    break;
            }
        }
        foreach (CombatPool combatPool in combatPoolsAux)
        {
            foreach (Node node in combatNodeList)
            {                
                if(node.CombatData == null)
                {
                    // If the node is in the range of the combat pool, it get a random combat from that pool
                    if(node.NodePos.y >= combatPool.depthRange.x && node.NodePos.y <= combatPool.depthRange.y)
                    {
                        node.CombatData = combatPool.combatsData[Random.Range(0,combatPool.combatsData.Count)];                        
                    }
                }                                
            }
        }
        combatNodeList.Clear();
        eventNodeList.Clear();
    }

    private void DeletePathlessNodes()
    {        
        List<Vector2> auxKeys = new List<Vector2>();
        foreach (Vector2 key in nodeDictionary.Keys)
        {            
            if(nodeDictionary[key].IsPathless && nodeDictionary[key].NodePos.y != height)
            {
                auxKeys.Add(key);
            }
        }
        foreach (Vector2 key in auxKeys)
        {
            nodeDictionary.Remove(key);
        }
        auxKeys.Clear();        
    }

    private void GeneratePath()
    {
        List<Node> path = new List<Node>();
        
        // Select random starting node from the first depth level
        Node startingNode;
        nodeDictionary.TryGetValue(new Vector2(Random.Range(0, width), 0), out startingNode);
        path.Add(startingNode);
        startingNode.IsPathless = false;
        int currentDepthLevel = 0;

        //Find the closest nodes
        while (currentDepthLevel < height-1)
        {
            List<Node> nearNodeList = new List<Node>();
            for (int i = 0; i < width; i++)
            {                
                //Debug.Log($"KEY: {i} {currentDepthLevel+1}");
                if (nodeDictionary.TryGetValue(new Vector2(i, currentDepthLevel + 1), out Node higherNode))
                {
                    //Debug.Log($"NODE HIGHER IN DEPTH {currentDepthLevel} : {higherNode.NodePos}");
                    if (NodesAreNear(startingNode, higherNode))
                    {
                        nearNodeList.Add(higherNode);
                    }
                }                
            }

            //Select a random node from the near nodes
            Node nextNode = nearNodeList[Random.Range(0, nearNodeList.Count)];

            //Add next node to the path set
            path.Add(nextNode);    
            startingNode.futureNodes.Add(nextNode);
            nextNode.IsPathless = false;            

            //Reset loop
            currentDepthLevel++;
            startingNode = nextNode;
        }
        pathList.Add(path);
    }

    private bool NodesAreNear(Node currentNode, Node higherNode)
    {
        return Vector2.Distance(currentNode.NodePos,higherNode.NodePos) < 2;       
    }

    private void GenerateNodes()
    {                
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                this.nodeDictionary.Add(new Vector2(j,i),new Node(j,i));
            }
        }        
    }
    #endregion

    #region Debug Methods
    public void ShowAllNodes()
    {
        foreach (var node in nodeDictionary)
        {
            Debug.Log($"{node.Key} --- {node.Value.IsPathless}");
        }
    }
    #endregion

    #region Getters and Setters
    public Dictionary<Vector2,Node> Nodes
    {
        get{ return nodeDictionary; }
    }
    public List<List<Node>> Paths
    {
        get { return pathList; }
    }
    public int Height
    {
        get { return height;}        
    }
    public int Width
    {
        get { return width;}
    }
    public Node Boss
    {
        get{ return bossNode; }
    }
    #endregion

}
