using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class AnvilStyleInfo : InfoText
{
    [field: SerializeField] public int styleLevel { get; protected set; }
    private void Awake()
    {
        AnvilUpgradeManager.Instance.OnEquipmentSelected += UpdateText;
    }

    private void UpdateText()
    {
        var stringBuilder = new StringBuilder();
        var equipment = AnvilUpgradeManager.Instance.selectedEquipment;
        if (equipment is WeaponData)
            stringBuilder.Append((string)Type.GetType(((WeaponData)equipment).weaponClassName.ToString())
                .GetMethod("GetStyleDescriptionByLevel").Invoke(null, new object[] { styleLevel }));

        if (styleLevel != 0) stringBuilder.Append($"\nCoste de la mejora a nivel {styleLevel}: {styleLevel * 15}");

        textToWrite = stringBuilder.ToString();
    }
}
