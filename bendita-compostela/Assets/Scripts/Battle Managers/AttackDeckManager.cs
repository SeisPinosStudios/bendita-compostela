using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackDeckManager : MonoBehaviour
{
    [field: SerializeField] public static AttackDeckManager Instance { get; private set; }
    [SerializeField] CardDataContainer cardPrefab;
    [SerializeField] Transform hand;
    [SerializeField] float drawCardDelay;
    [field: SerializeField] public List<WeaponAttackData> weaponAttacks { get; private set; }
    [field: SerializeField] public int costReduction { get; private set; }
    [field: SerializeField] public int freeDraw { get; private set; }
    /*====EVENTS====*/
    public event Action OnCardDraw = delegate { };

    private void Awake()
    {
        if (!Instance) Instance = this;
        EquipWeapon.OnEquipWeapon += FetchAttacks;
    }

    public void DrawFreeAttack(int amount)
    {
        StartCoroutine(DrawAttackCoroutine(amount));
    }

    public void DrawAttack()
    {
        if(freeDraw > 0)
        {
            freeDraw--;
            StartCoroutine(DrawAttackCoroutine(1));
            OnCardDraw();
            return;
        }

        if (!BattleManager.Instance.player.ConsumeEnergy(1)) return;
        StartCoroutine(DrawAttackCoroutine(1));
        OnCardDraw();
    }

    private IEnumerator DrawAttackCoroutine(int amount)
    {
        var weapon = BattleManager.Instance.player.weapon;
        for(int i = 0; i < amount; i++)
        {
            cardPrefab.cardData = weapon.attacks[UnityEngine.Random.Range(0, weapon.attacks.Count)];
            Instantiate(cardPrefab, hand);
            yield return new WaitForSeconds(drawCardDelay);
        }
    }

    private void FetchAttacks()
    {
        weaponAttacks = new List<WeaponAttackData>();
        foreach (WeaponAttackData attack in BattleManager.Instance.player.weapon.attacks) weaponAttacks.Add(attack.Copy());
    }
    public void ReduceAttackCost(int amount)
    {
        foreach (WeaponAttackData attack in weaponAttacks) attack.cost = Mathf.Clamp(attack.cost - costReduction, 0, attack.cost);
    }
    public void AddFreeDraw(int amount) { freeDraw += amount; }
}
