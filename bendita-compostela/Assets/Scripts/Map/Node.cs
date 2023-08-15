using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node{

    private GameObject nodeEvent;
    private int idx;    
    private int depth;
    private int width;

    private bool pathless;

    public HashSet<Node> futureNodes;     

    private Vector2 nodePos;
        
    public Node( int width,int depth)
    {                
        nodePos = new Vector2(width,depth);
        futureNodes = new HashSet<Node>();
        pathless = true;
    }

    public int Idx
    {
        get{ return idx;}
        set
        {
            idx = value;
        }
    }
    public GameObject NodeEvent
    {
        get{ return nodeEvent; }
        set
        {
            nodeEvent = value;   
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

    public override bool Equals(object obj)
    {
        return obj is Node node &&
               nodeEvent == node.nodeEvent &&
               idx == node.idx &&
               depth == node.depth &&
               width == node.width &&
               EqualityComparer<HashSet<Node>>.Default.Equals(futureNodes, node.futureNodes) &&
               nodePos.Equals(node.nodePos) &&
               NodePos.Equals(node.NodePos) &&
               Idx == node.Idx &&
               NodeEvent == node.NodeEvent;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(nodeEvent);
        hash.Add(idx);
        hash.Add(depth);
        hash.Add(width);
        hash.Add(futureNodes);
        hash.Add(nodePos);
        hash.Add(NodePos);
        hash.Add(Idx);
        hash.Add(NodeEvent);
        return hash.ToHashCode();
    }
}