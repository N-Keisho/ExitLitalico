using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhenExample : IhenBase
{
    void Start()
    {
        // 初期化処理
    }

    void Update()
    {
        // 異変関係なく毎フレーム実行される
    }

    protected override void DoIhen()
    {
        // 異変の処理
        // SetIhenするときに、isIhenがtrueになると実行
    }

}
