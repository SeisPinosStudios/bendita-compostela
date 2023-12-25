using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerociousRage : BasicPassive
{
    [field:SerializeField] public Enemy enemy { get; private set; }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDamage += PassiveEffect;
    }

    private void PassiveEffect()
    {
        enemy.AttackBonus(1);
    }

    private void OnDestroy()
    {
        enemy.OnDamage -= PassiveEffect;
    }

    #region Description
    public static string GetDescription()
    {
        return $"Rabia Feroz: cada ataque contra esta criatura aumenta su daño con ataques en 1";
    }
    #endregion
}
