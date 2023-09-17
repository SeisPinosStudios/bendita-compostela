using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class AnvilSynergyInfo : InfoText
{
    [field: SerializeField] public int synergyLevel { get; private set; }
    private void Awake()
    {
        AnvilUpgradeManager.Instance.OnEquipmentSelected += UpdateText;
    }

    private void UpdateText()
    {
        var stringBuilder = new StringBuilder();

        var equipment = AnvilUpgradeManager.Instance.selectedEquipment;
        if (equipment is ArmorData)
            stringBuilder.Append((string)Type.GetType(((ArmorData)equipment).weaponSynergyClass.ToString())
                .GetMethod("GetSynergyDescriptionByLevel").Invoke(null, new object[] { synergyLevel, ((ArmorData)equipment).armorType }));

        if (synergyLevel > 0) stringBuilder.Append($"\nCoste de la mejora a nivel {synergyLevel}: {10 + 10*synergyLevel}");

        textToWrite = stringBuilder.ToString();
    }
}
