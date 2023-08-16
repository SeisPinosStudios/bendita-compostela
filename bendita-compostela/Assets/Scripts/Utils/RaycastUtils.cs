using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class RaycastUtils
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns a RaycastResult with the first element hitted by the Raycast.</returns>
    public static RaycastResult Raycast()
    {
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0 ? results[0] : new RaycastResult();
    }

    public static RaycastResult Raycast(string tag)
    {
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        Debug.Log(pointerEventData.position);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        Debug.Log(results.Count);
        return results.Count > 0 && results[0].gameObject.transform.root.tag == tag ? results[0] : new RaycastResult();
    }

    public static GameObject Raycast2D(string tag)
    {
        var result = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        return result && result.transform.parent.tag == tag ? result.transform.parent.gameObject : null;
    }
}
