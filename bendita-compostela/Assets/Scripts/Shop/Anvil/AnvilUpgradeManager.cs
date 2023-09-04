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
            if (button.GetSiblingIndex() < weapon.weaponLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in weaponStyleButtons)
        {
            if (button.GetSiblingIndex() < weapon.styleLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in weaponUltiButtons)
        {
            if (button.GetSiblingIndex() < weapon.ultimateLevel) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }
    }
    public void UpdateArmorButtons()
    {
        var armor = (ArmorData)selectedEquipment;

        foreach (Transform button in armorSynergyButtons)
        {
            if (button.GetSiblingIndex() < armor.synergyLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }

        foreach (Transform button in armorDefenseButtons)
        {
            if (button.GetSiblingIndex() < armor.armorLevel+1) button.GetComponent<AnvilUpgradeSelector>().Disable();
            else button.GetComponent<AnvilUpgradeSelector>().Enable();
        }
    }
    #endregion

    #region Upgrades
    public void UpgradeWeaponDamage()
    {
        ((WeaponData)selectedEquipment).UpgradeWeaponLevel();
        UpdateWeaponButtons();
    }
    public void UpgradeWeaponStyle()
    {
        ((WeaponData)selectedEquipment).UpgradeStyleLevel();
        UpdateWeaponButtons();
        UpdateWeaponButtons();
    }
    public void UpgradeWeaponUlti()
    {
        ((WeaponData)selectedEquipment).UpgradeUltimateLevel();
        UpdateWeaponButtons();
    }
    public void UpdateArmorSynergy()
    {
        ((ArmorData)selectedEquipment).UpgradeSynergyLevel();
        UpdateArmorButtons();
    }
    public void UpgradeArmorLevel()
    {
        ((ArmorData)selectedEquipment).UpgradeArmorLevel();
        UpdateArmorButtons();
    }
    #endregion
}
