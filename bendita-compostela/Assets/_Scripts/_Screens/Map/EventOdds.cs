using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventOdds 
{
    public NodeEncounter nodeEncounter;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;
}

public enum NodeEncounter
{
    None,
    CombatEncounter,
    EventEncounter,
    ShopEncounter
}