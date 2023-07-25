using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffectsManager : MonoBehaviour
{
    [SerializeField] Entity entity;
    [field:SerializeField] public Dictionary<TAlteredEffects.AlteredEffects, int> alteredEffects = new Dictionary<TAlteredEffects.AlteredEffects, int>();
}
