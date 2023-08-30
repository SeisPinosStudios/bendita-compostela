using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentPoemSelector : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public PoemDataContainer poemDataCont { get; private set; }
    [field: SerializeField] public Image poemImage { get; private set; }

    private void Awake()
    {
        poemImage.sprite = poemDataCont.poemData.closedSprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var player = GameManager.Instance.playerData;
        if (player.poems.Count >= player.poemSlots) return;
        player.poems.Add(poemDataCont.poemData);
        EqPoemsDisplayManager.Instance.UpdateEquipedPoems();
        Destroy(gameObject);
    }

}
