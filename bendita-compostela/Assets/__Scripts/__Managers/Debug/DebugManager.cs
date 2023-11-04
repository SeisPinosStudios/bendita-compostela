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

    /// <summary>
    /// Static debug used to log messages into the console without the need of an Instance reference. This is the method that should be used in the other
    /// classes to debug messages. The method uses a dictionary of bools that checks if the desired type of messages will get logged or not. This
    /// is done to clean the console of debug messages and only log the desired type of information.
    /// </summary>
    /// <param name="type">The type of message you want to log</param>
    /// <param name="text">The message to be logged</param>
    public static void StaticDebug(string type, string text) {
        Instance.DebugLog(type, text);
    }
    public void DebugLog(string type, string text) {
        if (!checks.ContainsKey(type)) return;
        if (!checks[type]) return;
        Debug.Log(text);
    }
}
