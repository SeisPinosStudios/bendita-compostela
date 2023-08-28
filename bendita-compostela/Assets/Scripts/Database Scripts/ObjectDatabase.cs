using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SODataBase : MonoBehaviour
{
    [field: SerializeField] public static List<CardData> objects { get; private set; } = Resources.LoadAll<CardData>("Scriptable Objects/Cards/Objects").ToList();
}
