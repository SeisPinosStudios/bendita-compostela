using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New combat pool data", menuName = "Bendita Compostela/Enemy/New combat pool")]
public class CombatPool : ScriptableObject 
{
    [Header("Combat Pool")]    
    public Vector2 depthRange;
    public List<CombatData> combatsData = new List<CombatData>();
}