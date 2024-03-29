using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Player", menuName = "Bendita Compostela/Player Data")]
public class PlayerData : EntityData
{
    public List<CardData> deck = new List<CardData>();
    [field: SerializeField] public List<CardData> inventory { get; private set; } = new List<CardData>();
    public ArmorData chestArmor, legArmor;
    public int currentHP;
    [field: SerializeField] public RuntimeAnimatorController playerAnimator { get; private set; }
    [field:SerializeField, Header("Condecorations")] public List<CondecorationData> condecorations { get; private set; } = new List<CondecorationData>();
    [field: SerializeField, Header("Poems")] public List<PoemData> poems { get; private set; } = new List<PoemData>();
    [field: SerializeField] public List<PoemData> poemInventory { get; private set; } = new List<PoemData>();
    [field: SerializeField] public int poemSlots;
    [field: SerializeField, Header("Economy")] public int coins { get; private set; }

    public PlayerData Copy()
    {
        PlayerData player = CreateInstance<PlayerData>();

        foreach (CardData card in deck)
        {
            if(card is WeaponData)
            {
                player.deck.Add(((WeaponData)card).Copy());
                continue;
            }

            player.deck.Add(card.Copy());
        }

        foreach (CardData card in inventory)
        {
            if(card is WeaponData)
            {
                player.inventory.Add(((WeaponData)card).Copy());
                continue;
            }

            if(card is ArmorData)
            {
                player.inventory.Add(((ArmorData)card).Copy());
                continue;
            }

            player.inventory.Add(card.Copy());
        }

        player.chestArmor = chestArmor ? chestArmor.Copy() : null;
        player.legArmor = legArmor ? legArmor.Copy() : null;
        player.HP = HP;
        player.currentHP = currentHP;
        foreach (CondecorationData condecoration in condecorations) player.condecorations.Add(condecoration);
        foreach (PoemData poem in poems) player.poems.Add(poem);
        foreach (PoemData poem in poemInventory) player.poemInventory.Add(poem);
        player.poemInventory = poemInventory;
        player.poemSlots = poemSlots;
        player.coins = coins;
        player.playerAnimator = playerAnimator;

        return player;
    }
    public List<CardData> GetWeapons()
    {
        var weapons = new List<CardData>();

        weapons.AddRange(deck.Where(card => card.GetType() == typeof(WeaponData)));
        weapons.AddRange(inventory.Where(card => card.GetType() == typeof(WeaponData)));

        return weapons;
    }
    public List<CardData> GetArmors()
    {
        var armors = new List<CardData>();

        armors.AddRange(inventory.Where(card => card.GetType() == typeof(ArmorData)));
        if(chestArmor) armors.Add(chestArmor);
        if(legArmor) armors.Add(legArmor);

        return armors;
    }
    public bool SpendCoins(int amount)
    {
        if (coins < amount) return false;
        coins -= amount;
        return true;
    }
    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void ChangeCurrentHP(int amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 1, HP);
    }
    public void SetCurrentHP(int amount)
    {
        currentHP = amount;
    }
    public void ChangeMaxHP(int amount)
    {
        HP += amount;
    }
    public int GetDefense()
    {
        var defense = 0;
        if (chestArmor) defense += chestArmor.defenseBonus;
        if(legArmor) defense += legArmor.defenseBonus;
        return defense;
    }
    public void AddCondecoration(CondecorationData condecoration)
    {
        condecorations.Add(condecoration);
        Type.GetType(condecoration.type.ToString()).GetMethod("OnObtain").Invoke(null, null);
    }
}
