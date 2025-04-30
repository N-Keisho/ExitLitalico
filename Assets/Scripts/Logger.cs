using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour
{
    // C:\Users\[UserName]\AppData\LocalLow\N-Keisho\ExitLitalicoに保存
    private static string path = Application.persistentDataPath + "/Log.txt";
    public static void Log(string message)
    {
        #if UNITY_EDITOR
                Debug.Log(message);
        #endif
        File.AppendAllText(path, "[" + System.DateTime.Now.ToString() + "] " + message + "\n");
    }

    public static void Error(string message)
    {
        #if UNITY_EDITOR
                Debug.LogError(message);
        #endif
        File.AppendAllText(path, "[" + System.DateTime.Now.ToString() + "] Error: " + message + "\n");
    }
}
