using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlteredEffectDisplay : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI valueText;
    public TAlteredEffects.AlteredEffects effect;
    public int value;

    private void Awake()
    {
        image.sprite = sprites[(int)effect];
        valueText.text = value.ToString();
    }
}
