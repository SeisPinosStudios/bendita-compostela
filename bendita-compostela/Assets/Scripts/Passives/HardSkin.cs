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
}
