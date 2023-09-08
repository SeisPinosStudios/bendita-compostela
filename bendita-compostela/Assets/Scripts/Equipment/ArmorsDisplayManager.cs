using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorsDisplayManager : MonoBehaviour
{
    [field: SerializeField] public static ArmorsDisplayManager Instance { get; private set; }
    [field: SerializeField] public PlayerData playerData { get; private set; }
    [field: SerializeField] public Transform armorDisplay { get; private set; }
    [field: SerializeField] public CardDataContainer cardDataContainer { get; private set; }
    [field: SerializeField] public Image equipedChestDisplay { get; private set; }
    [field: SerializeField] public Image equipedLegDisplay { get; private set; }
    private void Awake()
    {
        Instance = this;
        StartCoroutine(SetupCoroutine()); 
    }

    private IEnumerator SetupCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance && GameManager.Instance.playerData);
        playerData = GameManager.Instance.playerData;
        ShowArmors();
    }

    private void ShowArmors()
    {
        foreach(ArmorData armor in playerData.GetArmors())
        {
            cardDataContainer.cardData = armor;
            var instObject = Instantiate(cardDataContainer, armorDisplay);
        }

        GetEquipedArmor();
    }

    public void GetEquipedArmor()
    {
        foreach(Transform child in armorDisplay)
        {
            var armor = (ArmorData)child.GetComponent<CardDataContainer>().cardData;
            if (armor == playerData.chestArmor || armor == playerData.legArmor) child.GetComponent<EquipmentArmorSelector>().Disable();
            else child.GetComponent<EquipmentArmorSelector>().Enable();
        }

        if(playerData.chestArmor) equipedChestDisplay.sprite = playerData.chestArmor.equipmentScreenIcon;
        if(playerData.legArmor) equipedLegDisplay.sprite = playerData.legArmor.equipmentScreenIcon;
    }
}
