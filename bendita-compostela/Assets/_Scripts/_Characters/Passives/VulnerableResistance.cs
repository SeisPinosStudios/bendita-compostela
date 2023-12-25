using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableResistance : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<EntityEffectsManager>().AddResistance(TAlteredEffects.AlteredEffects.Vulnerable);
    }

    public static string GetDescription()
    {
        return $"Resistencia al Vulnerable <sprite=2>";
    }
}
