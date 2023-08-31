using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatNodeEvent : MonoBehaviour
{
    [SerializeField] private NodeEvent nodeEvent;

    private void OnMouseDown()
    {
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);
        if(nodeEvent.nodeInfo.CombatData != null) Debug.Log("NODE COMBAT: " + nodeEvent.nodeInfo.CombatData.name);
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);
    }
}
