using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy data", menuName = "Bendita Compostela/New enemy")]
public class EnemyData : EntityData
{
    [field: SerializeField] public int damageMitigation { get; private set; }
    [field: SerializeField] public Sprite entitySprite { get; private set; }
    [field: SerializeField] public EnemyAttackSequence defaultSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence attackSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence healSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence dyingSequence { get; private set; }
}
