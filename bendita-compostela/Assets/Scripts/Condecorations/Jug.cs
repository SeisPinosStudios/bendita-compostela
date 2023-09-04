using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jug : MonoBehaviour
{
    public void Awake()
    {
        BattleManager.Instance.player.AddMaxEnergy(2);
    }
    public static void OnObtain()
    {
        
    }
}
