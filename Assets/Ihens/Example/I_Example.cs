using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// 何かしらの異変を起こすクラスの例
// </summary>
public class I_Example : IhenBase
{
    void Start()
    {
        // 異変の初期化処理
        _ihenDo = true; // 異変が起きている状態にする
        Debug.Log("Ihen_Example: 異変が起きました！");
    }

    void Update()
    {
        // いらないときは削除推奨（Updateは重い）
    }
}
