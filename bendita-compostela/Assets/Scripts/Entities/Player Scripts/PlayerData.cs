using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Bendita Compostela/New player data")]
public class PlayerData : EntityData
{
    public List<CardData> deck;
    public ArmorData chestArmor, legArmor;
    public int currentHP;
}
