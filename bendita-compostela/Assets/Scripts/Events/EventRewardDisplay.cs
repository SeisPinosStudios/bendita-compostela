using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventRewardDisplay : MonoBehaviour
{    
    [SerializeField] private TMP_Text textBox;

    public void SetTextReward(string rewardText)
    {
        textBox.text = rewardText;
    }
}
