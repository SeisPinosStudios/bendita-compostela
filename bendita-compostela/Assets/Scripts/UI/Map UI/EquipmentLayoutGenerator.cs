using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLayoutGenerator : MonoBehaviour
{
    [field: SerializeField] public GameObject equipmentLayout { get; private set; }
    [field: SerializeField] public GameObject inventoryLayout { get; private set; }

    public void GenerateEquipmentLayout()
    {
        Instantiate(equipmentLayout);
    }
    public void GenerateInventoryLayout()
    {
        Instantiate(inventoryLayout);
    }
}
