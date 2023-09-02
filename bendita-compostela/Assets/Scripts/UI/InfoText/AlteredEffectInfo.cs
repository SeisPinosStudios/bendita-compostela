using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AlteredEffectInfo : InfoText
{
    [field: SerializeField, Header("Altered Effect Info")] public AlteredEffectDisplay effectDisplay { get; private set; }
    private void Awake()
    {
        var entityEffManager = transform.parent.parent.GetComponentInParent<EntityEffectsManager>();
        var entity = transform.parent.parent.GetComponentInParent<Entity>();

        textToWrite = (string)Type.GetType(effectDisplay.effect.ToString()).GetMethod("GetDescription")
            .Invoke(null, new object[] { entityEffManager, entity});
    }

    public override IEnumerator OnPointerEnterCoroutine(PointerEventData eventData)
    {
        textBoxObject = Instantiate(textBoxPrefab, CanvasUtils.GetMainCanvas().transform);
        progressBar = textBoxObject.GetComponent<Image>();

        while (progress < 1)
        {
            progress += Time.deltaTime;
            progressBar.fillAmount = progress;
            print($"Current WaitTime {progress}");
            yield return null;
        }

        print($"Out of while loop {progress}");

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
