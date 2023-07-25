using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "BenditaCompostela/New card")]
public class CardData : ScriptableObject
{
    public enum Effect
    {
        Damage,
        Heal
    }

    [Header("Card Info")]
    public string cardName;
    [TextArea(5,10)]
    public string description;
    public int cost, price;
    public Sprite art, miniArt;

    [Header("Card Effects")]
    public List<Effect> cardEffects;
    public List<int> cardEffectsValues;
}
