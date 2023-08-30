using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNodeEncounter : MonoBehaviour
{
    [SerializeField] private NodeEvent nodeEvent;

    private void OnMouseDown()
    {
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);        
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);
    }
}
