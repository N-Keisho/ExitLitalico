using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IhenBase : MonoBehaviour
{
    [SerializeField] protected string _explanation = "初期値";
    public string Explanation { get { return _explanation; } }

    private void Awake()
    {
        string name = this.name.Replace("(Clone)", "");
        Debug.Log(name + " : " + _explanation);
        GV.SetDoneIhen(name);
    }
}