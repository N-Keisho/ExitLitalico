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
}
