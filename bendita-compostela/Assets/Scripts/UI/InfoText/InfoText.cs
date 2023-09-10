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
    [field:SerializeField, TextArea(3, 20)] public string textToWrite { get; protected set; }
    [field:SerializeField] public GameObject textBoxObject { get; protected set; }
    [field:SerializeField] public GameObject highlight { get; protected set; }
    [field: SerializeField] public float progress { get; protected set; }
    [field: SerializeField] public Image progressBar { get; protected set; }
    [field: SerializeField] public int loadingCircleSpeed { get; protected set; } = 1;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        progress = 0;
        if (highlight) highlight.SetActive(true);
        StartCoroutine(OnPointerEnterCoroutine(eventData));
    }

    public virtual IEnumerator OnPointerEnterCoroutine(PointerEventData eventData)
    {
        textBoxObject = Instantiate(textBoxPrefab, transform.root);
        progressBar = textBoxObject.GetComponent<Image>();

        while (progress < 1)
        {
            progress += Time.deltaTime * loadingCircleSpeed;
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

        if (eventData.position.x < Screen.width * 0.9) return;

        var pointerPosition = new Vector3(eventData.position.x - 200, eventData.position.y, 0);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform,
                                                                pointerPosition, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.GetChild(0).position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }

    protected void ShowTextBox()
    {
        textBoxObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
    
