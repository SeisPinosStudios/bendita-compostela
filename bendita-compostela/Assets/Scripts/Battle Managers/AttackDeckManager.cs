using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackDeckManager : MonoBehaviour
{
    [field: SerializeField] public static AttackDeckManager Instance { get; private set; }
    [SerializeField] CardDataContainer cardPrefab;
    [field: SerializeField] public Transform hand { get; private set; }
    [SerializeField] float drawCardDelay;
    [field: SerializeField] public List<WeaponAttackData> weaponAttacks { get; private set; }
    [field: SerializeField] public int costReduction { get; private set; }
    [field: SerializeField] public int freeDraw { get; private set; }
    [field: SerializeField] public bool ulti { get; private set; }
    /*====EVENTS====*/
    public event Action OnCardDraw = delegate { };

    private void Awake()
    {
        if (!Instance) Instance = this;
        EquipWeapon.OnEquipWeapon += () => StartCoroutine(FetchAttacks());
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
            var attack = weaponAttacks[UnityEngine.Random.Range(0, weaponAttacks.Count)];
            cardPrefab.cardData = attack;
            Instantiate(cardPrefab, hand);

            if (weapon.IsUlti(attack)) weaponAttacks.Remove(attack);

            yield return new WaitForSeconds(drawCardDelay);
        }
    }

    private IEnumerator FetchAttacks()
    {
        yield return new WaitUntil(() => BattleManager.Instance.player.weapon);
        Debug.Log($"Fetching Attacks");
        weaponAttacks = new List<WeaponAttackData>();
        foreach (WeaponAttackData attack in BattleManager.Instance.player.weapon.attacks) weaponAttacks.Add(attack.Copy());
        if (BattleManager.Instance.player.weapon.ultimateLevel > 0) weaponAttacks.Add(BattleManager.Instance.player.weapon.ultimate);
    }
    public void ReduceAttackCost(int amount)
    {
        foreach (WeaponAttackData attack in weaponAttacks) attack.Cost(-amount);
    }
    public void AddFreeDraw(int amount) { freeDraw += amount; }

    private void OnDestroy()
    {
        EquipWeapon.OnEquipWeapon -= () => StartCoroutine(FetchAttacks());
    }
}
