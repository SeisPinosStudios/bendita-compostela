using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy attack", menuName = "Bendita Compostela/Enemy attack")]
public class EnemyAttack : CardData
{
    public enum AttackType
    {
        Attack, Support
    }

    [field:SerializeField] public AttackType attackType { get; private set; }
}
