using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplay : EntityDisplay
{
    [Header("Player Display")] [SerializeField] Player player;
    [Header("Energy")] [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] Image energyBackground;
    [SerializeField] Image energyFill;
    float height = 0.22f;

    private void Update() {
        energyText.text = $"{player.energy}/{player.maxEnergy}";
        energyBackground.rectTransform.sizeDelta = new Vector2(0.18f * player.maxEnergy, height);
        energyFill.rectTransform.sizeDelta = new Vector2(0.18f * player.energy, height);
    }
}
