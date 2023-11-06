using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a static Instance of the class allowing scripts to access to the Instance without
/// a direct reference to a GameObject containing the Script.
/// </summary>
public abstract class StaticInstance<T>: MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// A singleton class has a single static instance in the whole game and will destroy any new class
/// created, leaving the Instance unmodified.
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {
    protected override void Awake() {
        if(Instance != null) Destroy(gameObject);
        base.Awake();
    }
}

/// <summary>
/// PersistenSingleton sets the GameObject containing the class as a persisten GameObject between scenes. Use
/// this type of singleton with GameManager and AudioManager that needs to exist always in the project.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour {
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
