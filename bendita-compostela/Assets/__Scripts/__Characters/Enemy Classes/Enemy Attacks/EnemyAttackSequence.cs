using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy sequence", menuName = "Bendita Compostela/Enemy sequence")]
public class EnemyAttackSequence : ScriptableObject
{
    public enum SequenceType { defaultSequence, attackSequence, healSequence, dyingSequence }
    [field: SerializeField] public List<EnemyAttack> attacks { get; private set; }
}
