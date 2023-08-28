using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondecorationData : MonoBehaviour
{
    public enum CondecorationType { Friend, Samael, Martyr, Honesty, Star, Jug}
    [field:SerializeField] public string condecorationName { get; private set; }
    [field:SerializeField] public string description { get; private set; }
    [field:SerializeField] public int id { get; private set; }
    [field:SerializeField] public CondecorationType type { get; private set; }
}
