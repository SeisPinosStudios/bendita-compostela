using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        player.chestArmor = chestArmor.Copy();
        player.legArmor = legArmor.Copy();
        player.HP = HP;
        player.currentHP = currentHP;
        player.condecorations = condecorations;
        player.poems = poems;
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
        armors.Add(chestArmor);
        armors.Add(legArmor);

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
}
