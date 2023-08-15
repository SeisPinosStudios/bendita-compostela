using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    [field:SerializeField] public EntityDataContainer entityDataContainer { get; protected set; }
    [field:SerializeField] public Entity entity { get; protected set; }
    [field: SerializeField] public bool isTurn { get; protected set; } = false;

    public virtual void OnTurnBegin()
    {
        
    }

    public virtual void OnTurn()
    {

    }

    public virtual void OnTurnEnd()
    {

    }
}
