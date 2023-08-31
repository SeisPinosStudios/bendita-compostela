using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InfoText : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [field: SerializeField] public GameObject textBoxPrefab { get; private set; }
    [field:SerializeField] public TextMeshProUGUI textObject { get; private set; }
    [field:SerializeField] public string textToWrite { get; protected set; }
    [field:SerializeField] public GameObject textBoxObject { get; protected set; }
    [field:SerializeField] public GameObject highlight { get; protected set; }
    private void Awake()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        textBoxObject = Instantiate(textBoxPrefab, CanvasUtils.GetMainCanvas().transform);
        textBoxObject.GetComponentInChildren<TextMeshProUGUI>().text = textToWrite;
        if (highlight) highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(textBoxObject);
        if(highlight) highlight.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform,
                                                                eventData.position, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }
}
    
