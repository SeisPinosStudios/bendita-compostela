using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeEncounter : MonoBehaviour
{
    [SerializeField] private NodeEvent nodeEvent;
    private void OnMouseDown()
    {
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);
        Debug.Log("NODE EVENT: " + nodeEvent.nodeInfo.EventPrefab);
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);        
    }
    private void OnMouseUp() 
    {
        Instantiate(nodeEvent.nodeInfo.EventPrefab);
    }
}
