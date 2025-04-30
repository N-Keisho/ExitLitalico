using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// ゲーム全体で使用する変数を管理するクラス
// PlayerPrefsを使用して、ゲームの設定を保存する
// </summary>
public class GV : MonoBehaviour
{
    static public float sensX_buf
    {
        get
        {
            return PlayerPrefs.GetFloat("sensX_buf", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("sensX_buf", value);
        }
    }
    static public float sensY_buf
    {
        get
        {
            return PlayerPrefs.GetFloat("sensY_buf", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("sensY_buf", value);
        }
    }
    static public float bobber_buf
    {
        get
        {
            return PlayerPrefs.GetFloat("bobber_buf", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("bobber_buf", value);
        }
    }

    public static bool isClear
    {
        get
        {
            return PlayerPrefs.GetInt("isClear", 0) == 1;
        }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("isClear", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isClear", 0);
            }
        }
    }

    static public bool IsDoneIhen(string name)
    {
        return PlayerPrefs.GetInt(name, 0) == 1;
    }

    static public void SetDoneIhen(string name, bool isDone = true)
    {
        if (isDone)
        {
            PlayerPrefs.SetInt(name, 1);
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
        }
    }
}
