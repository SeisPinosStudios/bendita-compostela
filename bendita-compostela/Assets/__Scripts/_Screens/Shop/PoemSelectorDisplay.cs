using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemSelectorDisplay : MonoBehaviour
{
    [SerializeField] PoemDataContainer poemDataContainer;
    [SerializeField] PoemData poemData;
    [field: SerializeField] public PoemSelector cardSelector { get; private set; }
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject display;
    [SerializeField] Animator animator;


    private void Awake()
    {

    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => poemDataContainer.poemData);
        poemData = poemDataContainer.poemData;
        sprite.sprite = poemData.closedSprite;
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    public void BuyAnimation()
    {
        display.SetActive(false);
        animator.Play("BuyCardAnimation");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);
    }
}
