using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LogManager : MonoBehaviour
{
    string logName = "";
    string path;
    [SerializeField] bool log = true;

    private void Awake()
    {
        path = $"{Directory.GetParent(Application.dataPath)}\\";
    }

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
        if (!log) return;

        if(logName.Equals(""))
        {
            System.IO.Directory.CreateDirectory(path + "Logs");
            logName = $"{path}Logs\\{Environment.UserName}_{System.DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            Debug.Log(logName);
        }

        try
        {
            System.IO.File.AppendAllText(logName, logString + "\n");
        }
        catch
        {
            Debug.Log($"Logging into file failed");
        }
    }
}
