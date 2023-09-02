using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InfoText : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [field: SerializeField, Header("Manual")] public GameObject textBoxPrefab { get; private set; }
    [field:SerializeField, Header("Automatic")] public TextMeshProUGUI textObject { get; private set; }
    [field:SerializeField] public string textToWrite { get; protected set; }
    [field:SerializeField] public GameObject textBoxObject { get; protected set; }
    [field:SerializeField] public GameObject highlight { get; protected set; }
    [field: SerializeField] public float progress { get; protected set; }
    [field: SerializeField] public Image progressBar { get; protected set; }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlight) highlight.SetActive(true);
        StartCoroutine(OnPointerEnterCoroutine(eventData));
    }

    public virtual IEnumerator OnPointerEnterCoroutine(PointerEventData eventData)
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

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        if (textBoxObject) Destroy(textBoxObject);
        if(highlight) highlight.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!textBoxObject) return;
        MoveTextToPointer(eventData);
    }

    protected virtual void MoveTextToPointer(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform,
                                                                eventData.position, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }

    protected void ShowTextBox()
    {
        textBoxObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
    
