using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EntityInfoText : InfoText
{
    public override IEnumerator OnPointerEnterCoroutine(PointerEventData eventData)
    {
        textBoxObject = Instantiate(textBoxPrefab, CanvasUtils.GetMainCanvas().transform);
        progressBar = textBoxObject.GetComponent<Image>();

        while (progress < 1)
        {
            progress += Time.deltaTime;
            progressBar.fillAmount = progress;
            yield return null;
        }

        progress = 0;
        progressBar.fillAmount = 0;

        MoveTextToPointer(eventData);
        ShowTextBox();
        textBoxObject.GetComponentInChildren<TextMeshProUGUI>().text = textToWrite;
        yield return null;
    }

    protected override void MoveTextToPointer(PointerEventData eventData)
    {
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasUtils.GetMainCanvas().transform as RectTransform,
                                                                pointerEventData.position, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }
}
