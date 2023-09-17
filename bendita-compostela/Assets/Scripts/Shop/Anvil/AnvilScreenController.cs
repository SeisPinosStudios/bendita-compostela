using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilScreenController : MonoBehaviour
{
    [field: SerializeField] public static AnvilScreenController Instance { get; private set; }
    [field: SerializeField] public CardData cardData { get; private set; }
    [field: SerializeField] public Transform weaponDisplay { get; private set; }
    [field: SerializeField] public GameObject weaponUpgradeScreen { get; private set; }
    [field: SerializeField] public Transform armorDisplay { get; private set; }
    [field: SerializeField] public GameObject armorUpgradeScreen { get; private set; }
    [field: SerializeField] public CardDataContainer equipmentSelectorPrefab { get; private set; }

    private void Awake()
    {
        Instance = this;

        foreach(CardData weapon in GameManager.Instance.playerData.GetWeapons())
        {
            equipmentSelectorPrefab.cardData = weapon;
            Instantiate(equipmentSelectorPrefab, weaponDisplay);
        }

        foreach(CardData armor in GameManager.Instance.playerData.GetArmors())
        {
            equipmentSelectorPrefab.cardData = armor;
            Instantiate(equipmentSelectorPrefab, armorDisplay);
        }
    }

    public void ShowArmorScreen()
    {
        armorUpgradeScreen.SetActive(true);
        weaponUpgradeScreen.SetActive(false);
    }

    public void ShowWeaponScreen()
    {
        armorUpgradeScreen.SetActive(false);
        weaponUpgradeScreen.SetActive(true);
    }
    public void SetInteraction()
    {
        ShopSelectionManager.Instance.EnableInteraction();
    }
}
