using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoemDataContainer : MonoBehaviour
{
    public PoemData poemData;
    [field:SerializeField] public PoemEffect poemEffect { get; private set; }

    private void Awake()
    {
        poemEffect = (PoemEffect)gameObject.AddComponent(Type.GetType(poemData.type.ToString()));
    }

    public void OnPoemUse()
    {
        Type.GetType(poemData.type.ToString()).GetMethod("Effect").Invoke(null, null);
        Destroy(gameObject);
    }
}
