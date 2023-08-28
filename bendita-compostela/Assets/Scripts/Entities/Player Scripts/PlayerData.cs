using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Bendita Compostela/New player data")]
public class PlayerData : EntityData
{
    public List<CardData> deck;
    [field:SerializeField] public List<CardData> inventory { get; private set; }
    public ArmorData chestArmor, legArmor;
    public int currentHP;
    [field:SerializeField, Header("Condecorations")] public List<CondecorationData> condecorations { get; private set; }
    [field:SerializeField, Header("Poems")] public List<PoemData> poems { get; private set; }

    public PlayerData Copy()
    {
        PlayerData player = CreateInstance<PlayerData>();

        foreach (CardData card in deck) player.deck.Add(card.Copy());
        foreach (CardData card in inventory) player.inventory.Add(card.Copy());
        player.chestArmor = chestArmor.Copy();
        player.legArmor = legArmor.Copy();
        player.currentHP = currentHP;
        player.condecorations = condecorations;
        player.poems = poems;

        return player;
    }
}
