using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUtils : MonoBehaviour
{
    public static List<CardData> Shuffle(List<CardData> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var randomPos = Random.Range(0, deck.Count);
            var temporalValue = deck[i];
            deck[i] = deck[randomPos];
            deck[randomPos] = temporalValue;
        }
        return deck;
    }
}
