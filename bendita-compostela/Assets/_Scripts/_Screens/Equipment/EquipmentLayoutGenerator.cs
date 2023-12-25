using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLayoutGenerator : MonoBehaviour
{
    [field: SerializeField] public GameObject equipmentLayout { get; private set; }
    [field: SerializeField] public GameObject inventoryLayout { get; private set; }

    public void GenerateEquipmentLayout()
    {
        equipmentLayout.GetComponent<Canvas>().worldCamera = Camera.main;
        equipmentLayout.GetComponent<Canvas>().sortingLayerName = "UI";
        Instantiate(equipmentLayout);
    }
    public void GenerateInventoryLayout()
    {
        inventoryLayout.GetComponent<Canvas>().worldCamera = Camera.main;
        inventoryLayout.GetComponent<Canvas>().sortingLayerName = "UI";
        Instantiate(inventoryLayout);
    }
}
