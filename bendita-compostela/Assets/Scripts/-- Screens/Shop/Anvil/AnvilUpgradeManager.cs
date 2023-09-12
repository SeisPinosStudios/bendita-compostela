using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnvilUpgradeManager : MonoBehaviour
{
    [field: SerializeField] public static AnvilUpgradeManager Instance { get; private set; }
    [field: SerializeField] public CardData selectedEquipment { get; private set; }
    [field: SerializeField] public Transform weaponDamageButtons { get; private set; }
    [field: SerializeField] public Transform weaponStyleButtons { get; private set; }
    [field: SerializeField] public Transform weaponUltiButtons { get; private set; }
    [field: SerializeField] public Transform armorSynergyButtons { get; private set; }
    [field: SerializeField] public Transform armorDefenseButtons { get; private set; }
    [field: SerializeField] public Image selectedEquipmentImage { get; private set; }
    [field: SerializeField] public List<GameObject> upgradeButtons {get; private set; }
    /*====EVENTS====*/
    public event Action OnEquipmentSelected = delegate { };

    private void Awake()
    {
        Instance = this;
    }

    public void SetEquipment(CardData equipment)
    {
        selectedEquipment = equipment;
        if (equipment is WeaponData) UpdateWeaponButtons();
        if (equipment is ArmorData) UpdateArmorButtons();
        selectedEquipmentImage.gameObject.SetActive(true);
        selectedEquipmentImage.sprite = selectedEquipment.miniArt;
        OnEquipmentSelected();
    }

    #region Update Buttons
    public void UpdateWeaponButtons()
    {
        var weapon = (WeaponData)selectedEquipment;
        foreach (Transform button in weaponDamageButtons)
        {
            if (!button.GetComponent<AnvilUpgradeSelector>()) continue;

            if (button.GetSiblingIndex() < weapon.weaponLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in weaponStyleButtons)
        {
            if (!button.GetComponent<AnvilUpgradeSelector>()) continue;

            if (button.GetSiblingIndex() < weapon.styleLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in weaponUltiButtons)
        {
            if (!button.GetComponent<AnvilUpgradeSelector>()) continue;

            if (button.GetSiblingIndex() < weapon.ultimateLevel) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        if(weapon.weaponLevel == 2) upgradeButtons[0].SetActive(false);
        else upgradeButtons[0].SetActive(true);

        if(weapon.styleLevel == 2) upgradeButtons[1].SetActive(false);
        else upgradeButtons[1].SetActive(true);

        if(weapon.ultimateLevel == 2) upgradeButtons[2].SetActive(false);
        else upgradeButtons[2].SetActive(true);
    }
    public void UpdateArmorButtons()
    {
        var armor = (ArmorData)selectedEquipment;

        foreach (Transform button in armorSynergyButtons)
        {
            if (!button.GetComponent<AnvilUpgradeSelector>()) continue;

            if (button.GetSiblingIndex() < armor.synergyLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in armorDefenseButtons)
        {
            if (!button.GetComponent<AnvilUpgradeSelector>()) continue;

            if (button.GetSiblingIndex() < armor.armorLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        if (armor.armorLevel == 2) upgradeButtons[3].SetActive(false);
        else upgradeButtons[3].SetActive(true);

        if (armor.synergyLevel == 2) upgradeButtons[4].SetActive(false);
        else upgradeButtons[4].SetActive(true);
    }
    #endregion

    #region Upgrades
    public void UpgradeWeaponDamage()
    {
        if(!GameManager.Instance.playerData.SpendCoins(15)) return;
        ((WeaponData)selectedEquipment).UpgradeWeaponLevel();
        UpdateWeaponButtons();
    }
    public void UpgradeWeaponStyle()
    {
        if(!GameManager.Instance.playerData.SpendCoins(15)) return;
        ((WeaponData)selectedEquipment).UpgradeStyleLevel();
        UpdateWeaponButtons();
    }
    public void UpgradeWeaponUlti()
    {
        if(!GameManager.Instance.playerData.SpendCoins(15)) return;
        ((WeaponData)selectedEquipment).UpgradeUltimateLevel();
        UpdateWeaponButtons();
    }
    public void UpdateArmorSynergy()
    {
        if(!GameManager.Instance.playerData.SpendCoins(15)) return;
        ((ArmorData)selectedEquipment).UpgradeSynergyLevel();
        UpdateArmorButtons();
    }
    public void UpgradeArmorLevel()
    {
        if(!GameManager.Instance.playerData.SpendCoins(15)) return;
        ((ArmorData)selectedEquipment).UpgradeArmorLevel();
        UpdateArmorButtons();
    }
    #endregion
}
