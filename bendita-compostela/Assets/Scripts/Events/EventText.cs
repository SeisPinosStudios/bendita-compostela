using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Text", menuName = "BenditaCompostela/Events/Event Text")]
public class EventText : ScriptableObject
{
    public List<string> text = new List<string>();
}