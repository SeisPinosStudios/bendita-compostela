using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour {
    [field: SerializeField] public EntityDataContainer entityDataContainer { get; protected set; }
    [field: SerializeField] public Entity entity { get; protected set; }
    [field: SerializeField] public EntityEffectsManager entityEffManager { get; protected set; }
    [field: SerializeField] public bool isTurn { get; protected set; } = false;
    [field: SerializeField] public BehaviourState state { get; protected set; }

    public virtual IEnumerator OnTurnBegin() {
        DebugManager.StaticDebug("Entity", $"{entity.entityData.entityName} OnTurnBegin");
        
        yield return StartCoroutine(entityEffManager.Poison());

        if(entity.IsDead()) yield break;

        if (entityEffManager.Suffering(TAlteredEffects.AlteredEffects.Stun)) {
            //player.entityDisplay.Stun();
            entityEffManager.RemoveEffect(TAlteredEffects.AlteredEffects.Stun, 1);
            ChangeBehaviourState(BehaviourState.OnTurnEnd);
            yield break;
        }
    }
    public virtual IEnumerator OnTurn() {
        DebugManager.StaticDebug("Entity", $"{entity.entityData.entityName} OnTurn");

        yield return null;
    }
    public virtual IEnumerator OnTurnEnd() {
        DebugManager.StaticDebug("Entity", $"{entity.entityData.entityName} OnTurnEnd");

        yield return StartCoroutine(entityEffManager.Burn());
        if(entity.IsDead()) yield break;
    }
    public virtual IEnumerator NoTurn() {
        yield return null;
    }

    public void ChangeBehaviourState(BehaviourState newState) {

        state = newState;
        switch (state) {
            case BehaviourState.OnTurnBegin:
                DebugManager.StaticDebug("Battle", $"{entity.entityData.entityName} OnTurnBegin");
                StartCoroutine(OnTurnBegin());
                break;
            case BehaviourState.OnTurn:
                StartCoroutine(OnTurn());
                break;
            case BehaviourState.OnTurnEnd:
                StartCoroutine(OnTurnEnd());
                break;
            case BehaviourState.NoTurn:
                StartCoroutine(NoTurn());
                break;
        }
    }
    public virtual void ChangeBehaviourState(int newState) {
        ChangeBehaviourState((BehaviourState)newState);
    }
}

public enum BehaviourState {
    OnTurnBegin,
    OnTurn,
    OnTurnEnd,
    NoTurn
}
