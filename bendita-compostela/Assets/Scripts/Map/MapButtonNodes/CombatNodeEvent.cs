using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatNodeEvent : MonoBehaviour
{
    [field: SerializeField] public NodeEvent nodeEvent { get; private set; }
    private void Start() 
    {
        //nodeEvent = GetComponent<NodeEvent>();
    }
    private void OnMouseDown()
    {
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);
        if(nodeEvent.nodeInfo.CombatData != null) Debug.Log("NODE COMBAT: " + nodeEvent.nodeInfo.CombatData.name);
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.SetCombat(nodeEvent.nodeInfo.CombatData);
        SceneManager.LoadScene("Battle");
    }
}
