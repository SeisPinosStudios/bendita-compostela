using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemInfo : InfoText
{
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GetComponent<PoemDataContainer>().poemData);
        textToWrite = GetComponent<PoemDataContainer>().poemData.description;
    }
}
