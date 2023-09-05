using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI text { get; private set; }

    private void Update()
    {
        text.text = GameManager.Instance.playerData.currentHP.ToString();
    }
}
