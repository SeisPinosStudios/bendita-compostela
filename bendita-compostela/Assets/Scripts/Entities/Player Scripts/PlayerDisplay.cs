using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDisplay : EntityDisplay
{
    [field: SerializeField, Header("Player Display")] public Player player;
    [field: SerializeField, Header("Energy")] public TextMeshProUGUI energyText;
    

    private void Update()
    {
        energyText.text = $"{player.energy}/{player.maxEnergy}";
    }

}
