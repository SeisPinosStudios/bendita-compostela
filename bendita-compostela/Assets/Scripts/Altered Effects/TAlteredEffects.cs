using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAlteredEffects : MonoBehaviour
{
    public enum AlteredEffects
    {
        Bleed, Poison, Vulnerable, Guarded, Invulnerable, Burn, Exhaust, Disarmed, Marked, Lead, Stun, Frenzy
    }
    
    [field:SerializeField] public static Dictionary<TAlteredEffects.AlteredEffects, int> alteredEffectsLimit = new Dictionary<AlteredEffects, int>()
        {
            {AlteredEffects.Bleed, 5},
            {AlteredEffects.Poison, 5},
            {AlteredEffects.Vulnerable, 1},
            {AlteredEffects.Guarded, 1},
            {AlteredEffects.Invulnerable, 1},
            {AlteredEffects.Burn, 10},
            {AlteredEffects.Exhaust, 5},
            {AlteredEffects.Disarmed, 5},
            {AlteredEffects.Marked, 1},
            {AlteredEffects.Lead, 1},
            {AlteredEffects.Stun, 1},
            {AlteredEffects.Frenzy, 1}
        };

    [field: SerializeField] public static List<AlteredEffects> negativeEffects = new List<AlteredEffects>()
    {
        {AlteredEffects.Bleed}, {AlteredEffects.Poison}, {AlteredEffects.Vulnerable}, {AlteredEffects.Burn}, {AlteredEffects.Exhaust}, {AlteredEffects.Disarmed},
        {AlteredEffects.Marked}, {AlteredEffects.Stun}
    };
}
