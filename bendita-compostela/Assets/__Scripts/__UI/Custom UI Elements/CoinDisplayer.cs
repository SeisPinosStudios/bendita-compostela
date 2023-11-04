using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplayer : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI coinText { get; private set; }

    private void Update()
    {
        coinText.text = GameManager.Instance.playerData.coins.ToString();
    }
}
