using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEvent : MonoBehaviour
{
    public Node nodeInfo;
    private SpriteRenderer sprRenderer;
    private BoxCollider2D nodeCollider;
    
    private void Start() {
        sprRenderer = GetComponent<SpriteRenderer>();
        nodeCollider = GetComponent<BoxCollider2D>();
    }
    private void OnMouseDown()
    {
        Debug.Log("NODE POS:" + nodeInfo.NodePos);
        if(nodeInfo.CombatData != null) Debug.Log("NODE COMBAT: " + nodeInfo.CombatData.name);
        if(nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeInfo);
    }
    private void Update() 
    {
        if(nodeCollider.enabled == false) 
        {
            sprRenderer.color = new Color(sprRenderer.color.r,sprRenderer.color.g,sprRenderer.color.b,0.5f);
        }
        else
        {
            sprRenderer.color = new Color(sprRenderer.color.r,sprRenderer.color.g,sprRenderer.color.b,1f);
        }
        
    }
}
