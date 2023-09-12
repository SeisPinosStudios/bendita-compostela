using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New poem", menuName = "Bendita Compostela/Poem")]
public class PoemData : ScriptableObject
{
    public enum PoemType { Heart, Sigh, Verses, PlusUltra, NoStepBack}

    [field:SerializeField, Header("Poem Data")] public string poemName { get; private set; }
    [field:SerializeField] public string description { get; private set; }
    [field: SerializeField] public int price { get; private set; }
    [field:SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Sprite closedSprite { get; private set; }
    [field:SerializeField] public int id { get; private set; }
    [field:SerializeField] public PoemType type { get; private set; }
}
