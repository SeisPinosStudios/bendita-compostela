using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogManager : MonoBehaviour
{
    string logName = "";
    string path = $"{Application.dataPath}/../";

    private void OnEnable()
    {
        Application.logMessageReceived += Log;
    }
    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        if(logName.Equals(string.Empty))
        {
            System.IO.Directory.CreateDirectory(path + "logs");
            logName = $"{path}logs/{Environment.UserName}_{System.DateTime.Now}";
        }

        try
        {
            System.IO.File.AppendAllText(logName, logString + "\n");
        }
        catch
        {
            DebugManager.Instance.DebugLog("System", $"Logging into file failed");
        }
    }
}
