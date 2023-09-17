using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveInfo : EntityInfoText
{
    [field: SerializeField] public PassiveDisplay passiveDisplay { get; private set; }
    private void Awake()
    {
        textToWrite = (string)Type.GetType(passiveDisplay.passive.ToString()).GetMethod("GetDescription").Invoke(null, null);
    }
}
