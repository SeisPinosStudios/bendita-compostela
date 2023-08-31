using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEvent : MonoBehaviour
{
    public Node nodeInfo;
    public bool isCompleted = false;
    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private BoxCollider2D nodeCollider; 
    [SerializeField] private Sprite completedNodeSprite;
        
    // TODO remake with delegates
    private void Update() 
    {
        if(nodeCollider.enabled == false) sprRenderer.color = new Color(sprRenderer.color.r,sprRenderer.color.g,sprRenderer.color.b,0.5f);        
        else sprRenderer.color = new Color(sprRenderer.color.r,sprRenderer.color.g,sprRenderer.color.b,1f);
        
        if(isCompleted) sprRenderer.sprite = completedNodeSprite;        
        
    }
}
