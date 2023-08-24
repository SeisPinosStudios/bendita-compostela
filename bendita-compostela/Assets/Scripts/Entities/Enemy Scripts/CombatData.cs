using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New combat data", menuName = "Bendita Compostela/Enemy/New combat")]
public class CombatData : ScriptableObject
{
    [Header("Combat Enemies")]        
    public List<EnemyData> enemiesData = new List<EnemyData>();
}