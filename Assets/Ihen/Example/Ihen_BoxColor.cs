using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// 色がかわるやーつ
// </summary>
public class Ihen_BoxColor : IhenBase
{
    [SerializeField] private Color color;
    protected override void DoIhen()
    {
        GetComponent<Renderer>().material.color = color;
    }
}
