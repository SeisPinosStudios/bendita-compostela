using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressDefense : BasicPassive
{
    [field: SerializeField] public int attacks { get; private set; }
    [field: SerializeField] public bool active { get; private set; }

    private void Awake()
    {
        GetComponent<Enemy>().OnDamage += PassiveEffect;
    }

    private void PassiveEffect()
    {
        if (!active) return;
        if (attacks == 4)
        {
            GetComponent<Enemy>().DefenseBonus(3);
            active = false;
            return;
        }
        attacks++;
    }

    private void OnDestroy()
    {
        GetComponent<Enemy>().OnDamage -= PassiveEffect;
    }

    #region Description
    public static string GetDescription()
    {
        return $"Defensa Express: al recibir 4 ataques aumenta su defensa en 3 puntos";
    }
    #endregion
}
