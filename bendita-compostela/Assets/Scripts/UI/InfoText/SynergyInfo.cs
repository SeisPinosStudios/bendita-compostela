using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class SynergyInfo : InfoText
{
    [field: SerializeField, Header("Synergy Info")] public Image image { get; private set; }
    [field: SerializeField] public bool leg { get; private set; }
    [field: SerializeField] public ArmorData armor { get; private set; }
    private string synergyDesc;

    private void Awake()
    {
        if (leg ? !GameManager.Instance.playerData.legArmor : !GameManager.Instance.playerData.chestArmor)
        {
            this.gameObject.SetActive(false);
            return;
        }

        armor = leg ? GameManager.Instance.playerData.legArmor : GameManager.Instance.playerData.chestArmor;
        var method = armor.armorType == 1 ? "GetLegDescription" : "GetChestDescription";

        image.sprite = armor.equipmentScreenIcon;
        image.color = new Color(0.5f, 0.5f, 0.5f);

        textToWrite = (string)Type.GetType(armor.weaponSynergyClass.ToString()).GetMethod(method).Invoke(null, null);

        EquipWeapon.OnEquipWeapon += CheckSynergy;
    }

    private void CheckSynergy()
    {
        if (armor.weaponSynergy == BattleManager.Instance.player.weapon.weaponId) image.color = new Color(1, 1, 1);
        else image.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public override IEnumerator OnPointerEnterCoroutine(PointerEventData eventData)
    {
        textBoxObject = Instantiate(textBoxPrefab, CanvasUtils.GetMainCanvas().transform);
        progressBar = textBoxObject.GetComponent<Image>();

        while (progress < 1)
        {
            progress += Time.deltaTime;
            progressBar.fillAmount = progress;
            yield return null;
        }

        progress = 0;
        progressBar.fillAmount = 0;

        MoveTextToPointer(eventData);
        ShowTextBox();
        textBoxObject.GetComponentInChildren<TextMeshProUGUI>().text = textToWrite;
        yield return null;
    }
    protected override void MoveTextToPointer(PointerEventData eventData)
    {
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasUtils.GetMainCanvas().transform as RectTransform,
                                                                pointerEventData.position, CanvasUtils.GetMainCanvas().worldCamera, out pos);
        textBoxObject.transform.position = CanvasUtils.GetMainCanvas().transform.TransformPoint(pos);
    }
    private void OnDestroy()
    {
        EquipWeapon.OnEquipWeapon -= CheckSynergy;
    }
}
