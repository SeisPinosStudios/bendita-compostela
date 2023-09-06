using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNodeEncounter : MonoBehaviour
{
    [SerializeField] private NodeEvent nodeEvent;
    private AsyncOperation sceneLoad;

    private void OnMouseUp()
    {
        Debug.Log("NODE POS:" + nodeEvent.nodeInfo.NodePos);        
        if(nodeEvent.nodeInfo.futureNodes.Count != 0)MapManager.Instance.NodeSelected(nodeEvent.nodeInfo);
        SceneManager.LoadScene("Shop");
    }

    public IEnumerator ToShopCoroutine()
    {
        StartCoroutine(LoadAsyncScene());
        yield return StartCoroutine(MapManager.Instance.GetComponent<FadeUtils>().FadeOutCoroutine(1f));
        sceneLoad.allowSceneActivation = true;
        yield return null;
    }

    private IEnumerator LoadAsyncScene()
    {
        sceneLoad = SceneManager.LoadSceneAsync("Shop");
        sceneLoad.allowSceneActivation = false;
        while (!sceneLoad.isDone)
        {
            Debug.Log($"Loading Battle Scene, progress: {sceneLoad.progress}");
            yield return null;
        }
    }
}
