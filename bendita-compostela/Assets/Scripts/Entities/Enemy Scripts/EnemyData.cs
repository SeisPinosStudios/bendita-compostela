using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy data", menuName = "Bendita Compostela/Enemy/Enemy")]

public class EnemyData : EntityData
{
    [field: SerializeField, Header("Enemy Stats")] public int damageMitigation { get; private set; }
    [field: SerializeField] public Sprite entitySprite { get; private set; }
    [field: SerializeField, Header("Passives")] public List<BasicPassive.Passive> passives { get; private set; }
    [field: SerializeField, Header("Attack Sequences")] public EnemyAttackSequence defaultSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence attackSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence healSequence { get; private set; }
    [field: SerializeField] public EnemyAttackSequence dyingSequence { get; private set; }
    [field: SerializeField] public bool isBoss { get; private set; }
}
