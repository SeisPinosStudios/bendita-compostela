using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ResourceSystem<T> where T : ScriptableObject {
    private static List<T> items = new List<T>();
    private static Dictionary<string, T> itemsDictionary = new Dictionary<string, T>();

    public virtual void PopulateList(string path) {
        items = Resources.LoadAll<T>(path).ToList();
    }
}
