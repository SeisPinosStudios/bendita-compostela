using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node{

    private NodeEncounter nodeEncounter;
    private int idx;    
    private int depth;
    private int width;
    private bool pathless;
    private CombatData combatData;
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

    public override bool Equals(object obj)
    {
        return obj is Node node &&
               nodeEncounter == node.nodeEncounter &&
               idx == node.idx &&
               depth == node.depth &&
               width == node.width &&
               EqualityComparer<HashSet<Node>>.Default.Equals(futureNodes, node.futureNodes) &&
               nodePos.Equals(node.nodePos) &&
               NodePos.Equals(node.NodePos) &&
               Idx == node.Idx &&
               NodeEncounter == node.nodeEncounter;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(nodeEncounter);
        hash.Add(idx);
        hash.Add(depth);
        hash.Add(width);
        hash.Add(futureNodes);
        hash.Add(nodePos);
        hash.Add(NodePos);
        hash.Add(Idx);
        hash.Add(NodeEncounter);
        return hash.ToHashCode();
    }
}