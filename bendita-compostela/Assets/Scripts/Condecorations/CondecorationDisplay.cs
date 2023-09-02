using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CondecorationDisplay : MonoBehaviour
{
    [field: SerializeField] public CondecorationDataContainer condecorationDataContainer { get; private set; }
    [field: SerializeField] public Image condecorationImage { get; private set; }

    private void Awake()
    {
        condecorationImage.sprite = condecorationDataContainer.condecorationData.sprite;
    }
}
