using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardSkin : BasicPassive
{
    private void Awake()
    {
        var entity = GetComponent<Entity>();
        entity.DefenseBonus(1);
    }

    #region Description
    public static string GetDescription()
    {
        return $"Piel Dura: sufre 1 punto menos de daño por ataques";
    }
    #endregion
}
