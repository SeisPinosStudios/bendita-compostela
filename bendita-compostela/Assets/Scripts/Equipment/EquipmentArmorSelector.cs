using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class EquipmentArmorSelector : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public CardDataContainer cardDataContainer { get; private set; }
    [field: SerializeField] public Image armorImage { get; private set; }
    [field: SerializeField] public bool interactable { get; private set; }

    private void Awake()
    {
        armorImage.sprite = ((ArmorData)cardDataContainer.cardData).equipmentScreenIcon;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        var armor = (ArmorData)cardDataContainer.cardData;
        if (armor.armorType == 0) GameManager.Instance.playerData.chestArmor = armor;
        else GameManager.Instance.playerData.legArmor = armor;

        ArmorsDisplayManager.Instance.GetEquipedArmor();
    }
    public void Disable()
    {
        armorImage.color = new Color(0.6f, 0.6f, 0.6f);
        interactable = false;
    }
    public void Enable()
    {
        armorImage.color = Color.white;
        interactable = true;
    }
}
