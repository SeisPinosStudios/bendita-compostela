using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;

public class EquipmentSummary : MonoBehaviour
{
    [field: SerializeField] public static EquipmentSummary Instance { get; private set; }
    [field: SerializeField] public TextMeshProUGUI chestSummary { get; private set; }
    [field: SerializeField] public TextMeshProUGUI legSummary { get; private set; }
    [field: SerializeField] public TextMeshProUGUI defenseSummary { get; private set; }

    private void Awake()
    {
        Instance = this;
        UpdateSummary();
    }

    public void UpdateSummary()
    {
        chestSummary.text = ChestSummary();
        legSummary.text = LegSummary();
        defenseSummary.text = ((GameManager.Instance.playerData.chestArmor ? GameManager.Instance.playerData.chestArmor.defenseBonus : 0) +
                                (GameManager.Instance.playerData.legArmor ? GameManager.Instance.playerData.legArmor.defenseBonus : 0)).ToString();
    }

    private string ChestSummary()
    {
        var summary = new StringBuilder();
        if (!GameManager.Instance.playerData.chestArmor) return "No hay pechera equipada.";

        var armor = GameManager.Instance.playerData.chestArmor;

        summary.Append($"{armor.cardName}\n");
        summary.Append($"Defensa: {armor.defenseBonus}\n");
        summary.Append($"Nivel de defensa: {armor.armorLevel} | Nivel de sinergia: {armor.synergyLevel}\n");
        summary.Append((string)Type.GetType(armor.weaponSynergyClass.ToString()).GetMethod("GetChestDescription").Invoke(null, null));

        return summary.ToString();
    }

    private string LegSummary()
    {
        var summary = new StringBuilder();
        if (!GameManager.Instance.playerData.legArmor) return "No hay pechera equipada.";

        var armor = GameManager.Instance.playerData.legArmor;

        summary.Append($"{armor.cardName}\n");
        summary.Append($"Defensa: {armor.defenseBonus}\n");
        summary.Append($"Nivel de defensa: {armor.armorLevel} | Nivel de sinergia: {armor.synergyLevel}\n");
        summary.Append((string)Type.GetType(armor.weaponSynergyClass.ToString()).GetMethod("GetLegDescription").Invoke(null, null));

        return summary.ToString();
    }

    private void OnDestroy()
    {
        
    }
}
