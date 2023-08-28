using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUtils : MonoBehaviour
{
    public int time;
    private bool isTimerFinished = false;
    private bool isTimerActive = false;    
    Coroutine timer;
    public void StartTimer() 
    {
        isTimerActive = true;
        timer = StartCoroutine(TimerCounter());
    }    
    public void StopTimer() 
    {        
        StopCoroutine(TimerCounter());
    }    
    public void SetTimer(int a) 
    {
        time = a;
    }
    IEnumerator TimerCounter() 
    {        
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            if (time <= 0)
            {                
                FinishTimer();
            }
        }        
    }
    void FinishTimer()
    {
        isTimerFinished = true;
        isTimerActive = false;
    }    
    public void ResetTimer() 
    {
        isTimerFinished = false;
        isTimerActive = false;
    }
    public bool IsTimerFinished()
    {
        return isTimerFinished;
    }
    public bool IsTimerActive() 
    {
        return isTimerActive;
    }
}
