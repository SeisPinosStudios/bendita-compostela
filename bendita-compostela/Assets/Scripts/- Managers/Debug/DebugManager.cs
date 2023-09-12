using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

public class DebugManager : MonoBehaviour
{
    [field: SerializeField] public static DebugManager Instance { get; private set; }
    [field: SerializeField] public SerializedDictionary<string, bool> checks { get; private set; } = new SerializedDictionary<string, bool>()
    {
        {"Battle", true }, {"Card", true}, {"Map", true }, {"Entity", true}, {"System", true}
    };

    private void Awake()
    {
        Instance = this;
    }

    public void DebugLog(string type, string text)
    {
        if (!checks.ContainsKey(type)) return;
        if (!checks[type]) return;
        Debug.Log(text);
    }
}
