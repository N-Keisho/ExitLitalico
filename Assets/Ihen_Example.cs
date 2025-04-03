using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// 何かしらの異変を起こすクラスの例
// </summary>
public class Ihen_Example : IhenBase
{
    void Update()
    {
        // 異変関係なく毎フレーム実行される
        // いらないときは削除推奨（Updateは重い）
    }

    protected override void DoIhen()
    {
        // 異変の処理
        // SetIhenで，isIhenがtrueのとき実行
        // Startに書きたいものはここに書く
        // StartやAwakeはisIhenがfalseのときも実行されてしまうので非推奨
    }

}
