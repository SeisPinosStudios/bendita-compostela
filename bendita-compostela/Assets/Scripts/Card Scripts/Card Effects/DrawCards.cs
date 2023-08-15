using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        DeckManager.Instance.DrawCard(int.Parse(data));
    }
}
