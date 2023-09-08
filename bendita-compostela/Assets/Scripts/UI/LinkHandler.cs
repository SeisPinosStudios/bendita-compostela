using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class LinkHandler : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] TMP_Text textBox;
    bool active;
    [SerializeField] GameObject textBoxPrefab;
    GameObject textBoxObject;
    Image progressBar;

    public void OnPointerMove(PointerEventData eventData)
    {
        var mousePos = new Vector3(eventData.position.x, eventData.position.y, 0);
        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(textBox, mousePos, Camera.main);

        if (linkTaggedText == -1) return;

        Debug.Log(textBox.textInfo.linkInfo[linkTaggedText].GetLinkID());
    }
    private IEnumerator LoadInfo()
    {
        var progress = 0f;
        while(progress < 1)
        {
            progress += Time.deltaTime * 1.5f;
            yield return null;
        }
    }
    private void DisableHover()
    {
        if (textBoxObject) Destroy(textBoxObject);
        active = false;
    }
}
