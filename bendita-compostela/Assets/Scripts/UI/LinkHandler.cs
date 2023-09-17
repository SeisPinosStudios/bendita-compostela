using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class LinkHandler : MonoBehaviour, IPointerMoveHandler
{
    TMP_Text textBox;
    bool active;
    [SerializeField] GameObject textBoxPrefab;
    GameObject textBoxObject;
    Image progressBar;
    string textToWrite;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var canvas = transform.root.GetComponent<Canvas>();
        var mousePos = new Vector3(eventData.position.x, eventData.position.y, 0);
        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(textBox, mousePos, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main);

        if (linkTaggedText == -1)
        {
            DisableHover();
            return;
        }

        if(!active) StartCoroutine(LoadInfo(linkTaggedText, eventData));

        MoveTextToPointer(eventData);
    }
    private IEnumerator LoadInfo(int index, PointerEventData eventData)
    {
        active = true;
        textBoxObject = Instantiate(textBoxPrefab, transform.root);
        progressBar = textBoxObject.GetComponent<Image>();
        textToWrite = (string)Type.GetType(textBox.textInfo.linkInfo[index].GetLinkID()).GetMethod("GetBasicDescription")
            .Invoke(null, null);

        var progress = 0f;
        while(progress < 1)
        {
            progress += Time.deltaTime * 2f;
            if(progressBar) progressBar.fillAmount = progress;
            yield return null;
        }
        //MoveTextToPointer(eventData);
        textBoxObject?.transform.GetChild(0).gameObject.SetActive(true);
        textBoxObject.GetComponentInChildren<TextMeshProUGUI>().text = textToWrite;
        progressBar.enabled = false;
        yield return null;
    }
    private void DisableHover()
    {
        if (textBoxObject) Destroy(textBoxObject);
        active = false;
    }
    protected virtual void MoveTextToPointer(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform,
                                                                eventData.position, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);

        var offset = 10f;
        var boxSize = new Vector2(textBoxObject.transform.GetChild(0).GetComponent<RectTransform>().rect.width,
                                    textBoxObject.transform.GetChild(0).GetComponent<RectTransform>().rect.height);

        var pointerPosition = new Vector3(eventData.position.x + boxSize.x/2, eventData.position.y, 0);

        if (eventData.position.x > Screen.width * 0.9)
            pointerPosition -= new Vector3(boxSize.x, 0, 0);

        if (eventData.position.y < Screen.height * 0.1)
            pointerPosition += new Vector3(0, boxSize.y, 0);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform,
                                                                pointerPosition, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.GetChild(0).position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }
}
