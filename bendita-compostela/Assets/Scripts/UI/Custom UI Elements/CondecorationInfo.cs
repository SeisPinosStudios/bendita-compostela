using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondecorationInfo : InfoText
{
    [field: SerializeField] public CondecorationDataContainer condDataContainer { get; private set; }

    private void Awake()
    {
        textToWrite = condDataContainer.condecorationData.description;
    }
}
