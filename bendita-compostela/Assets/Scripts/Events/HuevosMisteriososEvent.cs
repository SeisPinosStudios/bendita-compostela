using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuevosMisteriososEvent : BookEventController
{
    public void EggsEaten()
    {
        MapManager.Instance.SetBoss(1);
        MapManager.Instance.AsignBossEncounter();        
        ExitEvent();
    }
    public void ExitEvent()
    {
        Destroy(this);
    }
}
