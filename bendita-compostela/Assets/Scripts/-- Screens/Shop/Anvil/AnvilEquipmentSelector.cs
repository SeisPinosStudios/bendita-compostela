using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnvilEquipmentSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public CardDataContainer cardDataContainer { get; private set; }
    [field: SerializeField] public CardData cardData { get; private set; }
    [field: SerializeField] public Image display { get; private set; }
    [field: SerializeField] public GameObject highlight { get; private set; }
    [field: SerializeField] public int type { get; private set; } = 0;
    [field: SerializeField] public bool selected { get; private set; }

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
        display.sprite = cardData.miniArt;
        AnvilUpgradeManager.Instance.OnEquipmentSelected += CheckIfSelected;
        if (cardData.GetType() == typeof(ArmorData)) type = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (type == 0) AnvilScreenController.Instance.ShowWeaponScreen();
        else AnvilScreenController.Instance.ShowArmorScreen();

        AnvilUpgradeManager.Instance.SetEquipment(cardData);
        OnPointerExit(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void CheckIfSelected()
    {
        if (AnvilUpgradeManager.Instance.selectedEquipment == cardData)
        {
            display.color = new Color(0.8f, 0.8f, 0.8f);
            selected = true;
        }
        else
        {
            display.color = Color.white;
            selected = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected) return;
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }
}
