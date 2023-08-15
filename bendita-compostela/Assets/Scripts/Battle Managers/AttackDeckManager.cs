using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDeckManager : MonoBehaviour
{
    [field:SerializeField] public static AttackDeckManager Instance { get; private set; }
    [SerializeField] CardDataContainer cardPrefab;
    [SerializeField] Transform hand;
    [SerializeField] float drawCardDelay;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void DrawAttack(int amount)
    {
        StartCoroutine(DrawAttackCoroutine(amount));
    }

    private IEnumerator DrawAttackCoroutine(int amount)
    {
        var weapon = BattleManager.Instance.player.weapon;
        for(int i = 0; i < amount; i++)
        {
            cardPrefab.cardData = weapon.attacks[Random.Range(0, weapon.attacks.Count)];
            Instantiate(cardPrefab, hand);
            yield return new WaitForSeconds(drawCardDelay);
        }
    }
}
