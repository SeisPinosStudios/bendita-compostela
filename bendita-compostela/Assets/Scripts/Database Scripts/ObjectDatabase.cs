using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SODataBase : MonoBehaviour
{
    [field: SerializeField] public static List<CardData> objects { get; private set; } = Resources.LoadAll<CardData>("Scriptable Objects/Cards/Objects").ToList();

    [field: SerializeField] public static List<CardData> special { get; private set; } = Resources.LoadAll<CardData>("Scriptable Objects/Cards/Special").ToList();
    [field: SerializeField] public static List<WeaponData> weapons { get; private set; } = Resources.LoadAll<WeaponData>("Scriptable Objects/Cards/Weapons").ToList();
    [field: SerializeField] public static List<ArmorData> armors { get; private set; } = Resources.LoadAll<ArmorData>("Scriptable Objects/Cards/Armors").ToList();
    [field: SerializeField] public static List<CondecorationData> obtainableCondecorations { get; private set; } =
        Resources.LoadAll<CondecorationData>("Scriptable Objects/Condecorations").Where(condecoration => !condecoration.special).ToList();
    [field: SerializeField] public static List<PoemData> poems { get; private set; } = Resources.LoadAll<PoemData>("Scriptable Objects/Poems").ToList();
    [field: SerializeField] public static List<ArmorData> chestArmors { get; private set; } =
        Resources.LoadAll<ArmorData>("Scriptable Objects/Armors").Where(armor => armor.armorType == 0).ToList();

    [field: SerializeField] public static List<ArmorData> legArmors { get; private set; } =
    Resources.LoadAll<ArmorData>("Scriptable Objects/Armors").Where(armor => armor.armorType == 1).ToList();
}
