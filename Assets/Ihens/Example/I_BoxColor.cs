using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// 色がかわるやーつ
// </summary>
public class I_BoxColor : IhenBase
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Color color;
    void Start()
    {
        _targetObject.GetComponent<Renderer>().material.color = color;
    }
}
