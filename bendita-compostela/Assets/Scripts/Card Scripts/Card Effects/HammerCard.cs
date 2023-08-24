using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HammerCard : BasicCardEffect
{
    public static event Action onHammerCard;

    public static void Effec(string data, CardData card, GameObject user, GameObject target)
    {
        onHammerCard();
        var hammer = user.GetComponent<Hammer>();
        var entity = user.GetComponent<Entity>();
        if (hammer.styleAttacks < 5) return;
        foreach (Enemy enemy in BattleManager.Instance.enemies) 
            if (enemy != target.GetComponent<Enemy>()) 
                enemy.SufferDamage(int.Parse(card.GetDamage()), entity.damageBonus, entity.damageMultiplier,  false);
        hammer.ResetStyle();
    }
}
