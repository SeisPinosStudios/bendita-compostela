using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipWeapon : BasicCardEffect
{
    public static event Action OnEquipWeapon = delegate { };
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var player = BattleManager.Instance.player;
        var weapon = (WeaponData)card;
        if (player.weapon) 
        { 
            DeckManager.Instance.AddCardToDeck(player.weapon);
            UnityEngine.Object.Destroy(user.GetComponent<BaseWeapon>());
            foreach (Transform child in AttackDeckManager.Instance.hand)
                if (child.GetComponent<CardDataContainer>().cardData is WeaponAttackData)
                    GameObject.Destroy(child.gameObject);
        }
        BattleManager.Instance.player.SetWeapon((WeaponData)card);
        OnEquipWeapon();
        user.AddComponent(Type.GetType(weapon.weaponClassName.ToString()));
    }

    public static string GetDescription(CardData card, Entity user, Entity target)
    {
        return null;
    }
}
