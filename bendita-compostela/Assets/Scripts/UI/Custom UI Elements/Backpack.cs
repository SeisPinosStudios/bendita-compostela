using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class Backpack : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public Transform deckBuilder { get; private set; }
    [field: SerializeField] public Transform equipment { get; private set; }
    [field: SerializeField] public bool isOpen { get; private set; } = false;
    bool isbackpackOpen = false;

    [SerializeField] private Animator backpackAnimator;
    [SerializeField] private Animator backpackHoverAnimator;

    public event Action OnFinish;
    private void OnEnable() {
        OnFinish += ChangeBackpackState;
    }
    private void OnDisable() {
        OnFinish -= ChangeBackpackState;
    }        
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isOpen)
        {
            backpackAnimator.SetBool("isOpen", !isbackpackOpen);
            backpackHoverAnimator.SetBool("isOpen", !isbackpackOpen);            
            isOpen = true;
        }                
    }
    void ChangeBackpackState()
    {
        isOpen = !isOpen;
        isbackpackOpen = !isbackpackOpen;
    } 
    public void AnimationFinishedTrigger() => OnFinish?.Invoke();
}
