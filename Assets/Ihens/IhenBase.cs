using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IhenBase : MonoBehaviour
{
    [Header("IhenBase Param")]
    [SerializeField] protected string _explanation = "初期値";
    public string Explanation { get { return _explanation; } }

    private void Awake()
    {
        string name = this.name.Replace("(Clone)", "");
        // Logger.Log(name + " : " + _explanation);
        Logger.Log(name + " : " + _explanation);
        GV.SetDoneIhen(name);
    }
}