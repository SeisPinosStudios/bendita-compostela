using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StyleInfo : InfoText
{
    [field: SerializeField, Header("Style Info")] public Image image { get; private set; }
    [field: SerializeField] public List<Sprite> sprites { get; private set; }
    private void Awake()
    {
        EquipWeapon.OnEquipWeapon += UpdateDescription;
    }

    private void UpdateDescription()
    {
        image.enabled = true;
        image.sprite = sprites[BattleManager.Instance.player.weapon.weaponId];
        textToWrite = (string)Type.GetType(BattleManager.Instance.player.weapon.weaponClassName.ToString())
            .GetMethod("GetStyleDescription").Invoke(null, new object[] {BattleManager.Instance.player.weapon});
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
        EquipWeapon.OnEquipWeapon -= UpdateDescription;
    }
}
