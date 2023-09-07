using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemSelector : MonoBehaviour
{
    [field: SerializeField] public PoemDataContainer poemDataContainer { get; private set; }
    [field: SerializeField] public PoemData poemData { get; private set; }
    [field: SerializeField] public PoemSelectorDisplay poemDisplay { get; private set; }
    [field: SerializeField] public bool interact { get; private set; }
    [field: SerializeField] public PolygonCollider2D poemCollider { get; private set; }
    
    public IEnumerator Start()
    {
        yield return new WaitUntil(() => poemDataContainer.poemData);
        poemData = poemDataContainer.poemData;
    }

    private void OnMouseUp()
    {
        if (!interact) return;

        if (!GameManager.Instance.playerData.SpendCoins(poemData.price)) return;

        GameManager.Instance.playerData.poemInventory.Add(poemData);
        poemDisplay.BuyAnimation();
    }

    public void Disable()
    {
        poemCollider.enabled = false;
        interact = false;
    }

    public void Enable()
    {
        poemCollider.enabled = true;
        interact = true;
    }
}
