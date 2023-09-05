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
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);
        if(nodeEvent.nodeInfo.CombatData != null) Debug.Log("NODE COMBAT: " + nodeEvent.nodeInfo.CombatData.name);
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);

        GameManager.Instance.SetCombat(nodeEvent.nodeInfo.CombatData);
        StartCoroutine(ToCombatCoroutine());
        //SceneManager.LoadScene("Battle");
    }

    public IEnumerator ToCombatCoroutine()
    {
        StartCoroutine(LoadAsyncScene());
        //yield return MapManager.Instance.GetComponent<FadeUtils>().FadeOut(1.0f);
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
