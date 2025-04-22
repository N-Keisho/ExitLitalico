using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IhenBase : MonoBehaviour
{
    protected bool _ihenDo = false;
    [SerializeField] protected string _explanation = "初期値";
    public string Explanation { get { return _explanation; } }
}
