using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipWeapon : BasicCardEffect
{
    public static event Action onEquipWeapon = delegate { };
    public static void Effect(string data, CardData card, GameObject user, GameObject target)
    {
        var player = BattleManager.Instance.player;
        var weapon = (WeaponData)card;
        if (player.weapon) 
        { 
            DeckManager.Instance.AddCardToDeck(player.weapon);
            UnityEngine.Object.Destroy(user.GetComponent<BaseWeapon>());
        }
        BattleManager.Instance.player.SetWeapon((WeaponData)card);
        user.AddComponent(Type.GetType(weapon.weaponClassName.ToString()));
        onEquipWeapon();
    }
}
