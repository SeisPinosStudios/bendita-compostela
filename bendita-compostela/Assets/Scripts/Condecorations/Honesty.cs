using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honesty : MonoBehaviour
{
    public static void OnObtain()
    {
        GameManager.Instance.playerData.ChangeMaxHP(7);
    }
}
