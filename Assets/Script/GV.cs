using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// ゲーム全体で使用する変数を管理するクラス
// PlayerPrefsを使用して、ゲームの設定を保存する
// </summary>
public class GV : MonoBehaviour
{
    [SerializeField] private float _ms;
    [SerializeField] private float _ds;
    [SerializeField] private float _sx;
    [SerializeField] private float _sy;
    [SerializeField] private float _my;
    [SerializeField] private float _xy;
    private void Start()
    {
        // ゲーム開始時にそれぞれに代入
        moveSpeed = _ms;
        dashSpeed = _ds;
        sensX = _sx;
        sensY = _sy;
        minY = _my;
        maxY = _xy;
    }
    static public float moveSpeed
    {
        get
        {
            return PlayerPrefs.GetFloat("moveSpeed", 5f);
        }
        set
        {
            PlayerPrefs.SetFloat("moveSpeed", value);
        }
    }
    static public float dashSpeed
    {
        get
        {
            return PlayerPrefs.GetFloat("dashSpeed", 10f);
        }
        set
        {
            PlayerPrefs.SetFloat("dashSpeed", value);
        }
    }
    static public float sensX
    {
        get
        {
            return PlayerPrefs.GetFloat("sensX", 15f);
        }
        set
        {
            PlayerPrefs.SetFloat("sensX", value);
        }
    }
    static public float sensY
    {
        get
        {
            return PlayerPrefs.GetFloat("sensY", 15f);
        }
        set
        {
            PlayerPrefs.SetFloat("sensY", value);
        }
    }
    static public float minY
    {
        get
        {
            return PlayerPrefs.GetFloat("miniY", -60f);
        }
        set
        {
            PlayerPrefs.SetFloat("miniY", value);
        }
    }
    static public float maxY
    {
        get
        {
            return PlayerPrefs.GetFloat("maxiY", 60f);
        }
        set
        {
            PlayerPrefs.SetFloat("maxiY", value);
        }
    }

}
