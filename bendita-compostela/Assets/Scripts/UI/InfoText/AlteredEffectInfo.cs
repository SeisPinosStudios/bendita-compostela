using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AlteredEffectInfo : EntityInfoText
{
    [field: SerializeField, Header("Altered Effect Info")] public AlteredEffectDisplay effectDisplay { get; private set; }
    private void Awake()
    {
        var entityEffManager = transform.parent.parent.GetComponentInParent<EntityEffectsManager>();
        var entity = transform.parent.parent.GetComponentInParent<Entity>();

        textToWrite = (string)Type.GetType(effectDisplay.effect.ToString()).GetMethod("GetDescription")
            .Invoke(null, new object[] { entityEffManager, entity});
    }
}
