using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondecorationInfo : InfoText
{
    [field: SerializeField, Header("Condecoration")] public CondecorationDataContainer condDataContainer { get; private set; }

    private void Awake()
    {
        textToWrite = condDataContainer.condecorationData.description;
    }
}
