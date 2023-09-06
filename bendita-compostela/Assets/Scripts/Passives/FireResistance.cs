using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireResistance : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<EntityEffectsManager>().AddResistance(TAlteredEffects.AlteredEffects.Burn);
    }

    public static string GetDescription()
    {
        return $"Resistencia al Quemado <sprite=5>";
    }
}
