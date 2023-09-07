using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NodeEvent : MonoBehaviour
{
    public Node nodeInfo;
    public bool isCompleted = false;
    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private BoxCollider2D nodeCollider;                     
    public void DisableUncompletedNode()
    {
        isCompleted = false;
        nodeCollider.enabled = false;
        sprRenderer.color = new Color(0.5660378f,0.5660378f,0.5660378f,1f);
    }
    public void NodeIsSelectable()
    {
        sprRenderer.color = new Color(1,1,1,1);
        nodeCollider.enabled = true;
        GetComponent<FadeUtils>()?.StartLoopFade();        
    }
    public void NodeIsCompleted()
    {
        isCompleted = true;
        nodeCollider.enabled = false;
        sprRenderer.color = new Color(1,1,1,1);
    }
}
