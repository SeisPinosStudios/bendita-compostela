using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicCardEffect
{
    public static void Effect(string data, CardData card, GameObject user)
    {
        Debug.Log("Basic Card Use");
    }
}
