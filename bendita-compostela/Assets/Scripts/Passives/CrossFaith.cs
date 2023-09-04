using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFaith : BasicPassive
{
    private void Awake()
    {
        Damage.OnAttack2 += PassiveEffect;
    }
    private void PassiveEffect(GameObject target, GameObject user, CardData card)
    {
        if (user.GetComponent<Entity>() != this.GetComponent<Entity>()) return;
        var entity = GetComponent<Entity>();
        if (Random.Range(0, 2) > 0) entity.RestoreHealth(Mathf.RoundToInt(card.GetDamage() / 2), entity.healingBonus, entity.healingMultiplier);
    }

    private void OnDestroy()
    {
        Damage.OnAttack2 -= PassiveEffect;
    }
}
