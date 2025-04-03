using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// 色がかわるやーつ
// </summary>
public class IhenBox : IhenBase
{
    [SerializeField] private Color color;
    private Renderer _renderer;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 異変関係なく毎フレーム実行される
        // 何かの処理をする場合は、IhenStart()で行うこと
    }

    protected override void DoIhen()
    {
        // 異変の処理
        // SetIhenするときに、isIhenがtrueになると実行
        _renderer.material.color = color;
    }
}
