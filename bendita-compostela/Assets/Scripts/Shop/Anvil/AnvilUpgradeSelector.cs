using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnvilUpgradeSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum UpgradeType { Damage, Style, Ulti, Level, Synergy}
    [field: SerializeField] public UpgradeType upgradeType { get; private set; }
    [field: SerializeField] public Image upgradeIcon { get; private set; }
    [field: SerializeField] public List<Sprite> icons { get; private set; }
    [field: SerializeField] public GameObject highlight { get; private set; }
    [field: SerializeField] public Image highlighImage { get; private set; }
    [field: SerializeField] public bool interactionEnabled { get; private set; }
    [field: SerializeField] public int cost { get; private set; }

    private void Awake()
    {
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitUntil(() =>  AnvilUpgradeManager.Instance);
        AnvilUpgradeManager.Instance.OnEquipmentSelected += UpdateIcons;
        UpdateIcons();
    }

    private void UpdateIcons()
    {
        var equipment = AnvilUpgradeManager.Instance.selectedEquipment;
        if (equipment is WeaponData)
        {
            upgradeIcon.sprite = icons[((WeaponData)equipment).weaponId];
        }
        if (equipment is ArmorData) upgradeIcon.sprite = icons[((ArmorData)equipment).armorId];
    }
    public void Disable()
    {
        interactionEnabled = false;
        upgradeIcon.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void Enable()
    {
        interactionEnabled = true;
        upgradeIcon.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.playerData.SpendCoins(cost)) return;

        if (!interactionEnabled) return;

        switch (upgradeType)
        {
            case UpgradeType.Damage:
                AnvilUpgradeManager.Instance.UpgradeWeaponDamage();
                return;
            case UpgradeType.Style:
                AnvilUpgradeManager.Instance.UpgradeWeaponStyle();
                return;
            case UpgradeType.Ulti:
                AnvilUpgradeManager.Instance.UpgradeWeaponUlti();
                return;
            case UpgradeType.Level:
                AnvilUpgradeManager.Instance.UpgradeArmorLevel();
                return;
            case UpgradeType.Synergy:
                AnvilUpgradeManager.Instance.UpdateArmorSynergy();
                return;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    private void OnDestroy()
    {
        AnvilUpgradeManager.Instance.OnEquipmentSelected -= UpdateIcons;
    }

}
