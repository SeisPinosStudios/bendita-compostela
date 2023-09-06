using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [field: SerializeField] public static ShopManager Instance;
    [field: SerializeField] public Transform weaponSelectors { get; private set; }
    [field: SerializeField] public Transform armorSelectors { get; private set; }
    [field: SerializeField] public Transform objectSelectors { get; private set; }
    [field: SerializeField] public Transform specialSelectors { get; private set; }
    [field: SerializeField, Header("Shop Music")] private AudioClip shopMusic;
    
    private void Awake()
    {
        Instance = this;

        foreach (Transform selector in weaponSelectors)
            selector.GetComponent<CardDataContainer>().cardData = SODataBase.weapons[Random.Range(0, SODataBase.weapons.Count)];

        foreach (Transform selector in armorSelectors)
            selector.GetComponent<CardDataContainer>().cardData = SODataBase.armors[Random.Range(0, SODataBase.armors.Count)];

        foreach (Transform selector in objectSelectors)
            selector.GetComponent<CardDataContainer>().cardData = SODataBase.objects[Random.Range(0, SODataBase.objects.Count)];

        foreach (Transform selector in specialSelectors)
            selector.GetComponent<CardDataContainer>().cardData = SODataBase.special[Mathf.FloorToInt(Random.Range(0, SODataBase.special.Count) / 3)];
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => SoundManager.Instance);
        SoundManager.Instance.PlayMusic(shopMusic);
    }
}
