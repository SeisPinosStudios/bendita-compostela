using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatNodeEvent : MonoBehaviour
{
    [SerializeField] private NodeEvent nodeEvent;
    private AsyncOperation sceneLoad;

    private void OnMouseDown()
    {
        DebugManager.StaticDebug("Map", "NODE POS:" + nodeEvent.nodeInfo.NodePos);

        if (GameManager.Instance.playerData.deck.Count < 6 || !GameManager.Instance.playerData.deck.Find(x => x.GetType() == typeof(WeaponData))) return;

        if (nodeEvent.nodeInfo.CombatData != null) DebugManager.StaticDebug("Map", "NODE COMBAT: " + nodeEvent.nodeInfo.CombatData.name);
        if (nodeEvent.nodeInfo.futureNodes.Count != 0) MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);

        GameManager.Instance.SetCombat(nodeEvent.nodeInfo, nodeEvent.nodeInfo.CombatData);
        StartCoroutine(ToCombatCoroutine());
        //SceneManager.LoadScene("Battle");
    }

    public IEnumerator ToCombatCoroutine()
    {
        StartCoroutine(LoadAsyncScene());
        yield return StartCoroutine(MapManager.Instance.GetComponent<FadeUtils>().FadeOutCoroutine(1f));
        MapManager.Instance.EneableNextAvailableNodes(nodeEvent.nodeInfo);
        sceneLoad.allowSceneActivation = true;
        yield return null;
    }

    private IEnumerator LoadAsyncScene()
    {
        sceneLoad = SceneManager.LoadSceneAsync("Battle");
        sceneLoad.allowSceneActivation = false;
        while (!sceneLoad.isDone)
        {
            Debug.Log($"Loading Battle Scene, progress: {sceneLoad.progress}");
            yield return null;
        }
    }
}
