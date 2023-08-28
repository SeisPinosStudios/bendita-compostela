using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node{

    [SerializeField] private NodeEncounter nodeEncounter;
    private int idx;    
    private int depth;
    private int width;
    private bool pathless;
    [SerializeField] private CombatData combatData;
    [SerializeField] private GameObject eventPrefab;
    public HashSet<Node> futureNodes;     

    private Vector2 nodePos;
        
    public Node( int width,int depth)
    {                
        nodePos = new Vector2(width,depth);
        futureNodes = new HashSet<Node>();
        pathless = true;
        nodeEncounter = NodeEncounter.None;
    }

    public int Idx
    {
        get{ return idx;}
        set
        {
            idx = value;
        }
    }
    public NodeEncounter NodeEncounter
    {
        get{ return nodeEncounter; }
        set
        {
            nodeEncounter = value;   
        }
    }
    public Vector2 NodePos
    {
        get{ return nodePos;}
    }
    public bool IsPathless
    {
        get { return pathless;}
        set 
        {
            pathless = value;
        }
    }
    public CombatData CombatData
    {
        get{ return combatData;}
        set
        {
            combatData = value;
        }
    }    
    public GameObject EventPrefab
    {
        get { return eventPrefab; }
        set
        {
            eventPrefab = value;
        }
    }
}