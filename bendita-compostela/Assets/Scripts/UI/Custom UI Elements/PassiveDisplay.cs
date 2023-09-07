using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveDisplay : MonoBehaviour
{
    [field: SerializeField] public List<Sprite> sprites { get; private set; }
    [field: SerializeField] public Image image { get; private set; }
    [field: SerializeField] public BasicPassive.Passive passive;

    private void Awake()
    {
        image.sprite = sprites[(int)passive];
    }
}
