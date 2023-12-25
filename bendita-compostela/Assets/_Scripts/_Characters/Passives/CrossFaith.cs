using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFaith : BasicPassive
{
    private void Awake()
    {
        Damage.OnAttack += PassiveEffect;
    }
    private void PassiveEffect(GameObject target, GameObject user, CardData card)
    {
        if (user.GetComponent<Entity>() != this.GetComponent<Entity>()) return;
        var entity = GetComponent<Entity>();
        if (Random.Range(0, 2) > 0) entity.RestoreHealth(Mathf.RoundToInt(card.GetDamage() / 2), entity.healingBonus, entity.healingMultiplier);
    }

    private void OnDestroy()
    {
        Damage.OnAttack -= PassiveEffect;
    }

    #region Description
    public static string GetDescription()
    {
        return $"Bajo la F� de la Cruz: 50% de probabilidad de recuperar el 50% del da�o de un ataque";
    }
    #endregion
}
