using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New condecoration", menuName = "Bendita Compostela/Condecoration")]
public class CondecorationData : ScriptableObject
{
    public enum CondecorationType { Friend, Samael, Martyr, Honesty, Star, Jug}
    [field:SerializeField] public string condecorationName { get; private set; }
    [field:SerializeField, TextArea(3, 20)] public string description { get; private set; }
    [field:SerializeField] public Sprite sprite { get; private set; }
    [field:SerializeField] public int id { get; private set; }
    [field:SerializeField] public CondecorationType type { get; private set; }
    [field : SerializeField] public bool special { get; private set; }
}
