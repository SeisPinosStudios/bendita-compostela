using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDeck : InfoText
{
    [field: SerializeField] public List<Sprite> deckSprites { get; private set; }
    [field: SerializeField] public Image deckImage { get; private set; }

    private void Awake()
    {
        EquipWeapon.OnEquipWeapon += UpdateImage;
    }

    private void UpdateImage()
    {
        deckImage.sprite = deckSprites[BattleManager.Instance.player.weapon.weaponId];
    }

    private void OnDestroy()
    {
        EquipWeapon.OnEquipWeapon -= UpdateImage;
    }
}
