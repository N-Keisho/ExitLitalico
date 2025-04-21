# ワンダー町田から脱出せよ！

ごにきが作ってくれたワンダー町田のモデルを基に開発した「8 番出口」ライクなゲームです．

## ゲーム説明

### 基本操作

- 移動：WASD or 左スティック
- 視点：マウス移動 or 右スティック
- ダッシュ：Shift or 左右トリガー（R2L2）
- ズーム：マウス中央押し込み or 左右ショルダー（R1L1）
- ポーズ画面：Tab or Homeボタン or Startボタン or Selectボタン

### ルール

- 異変を見逃さないこと
- 異変を見つけたら，すぐに引き返すこと（出入り口から戻る）
- 異変がみつからなかったら，引き返さないこと（休憩口に進む）
- ワンダー町田から外に出ること（8回）

## 開発メモ

### コード規則

- 変数名は小文字スタートのキャメルケース，クラス名や関数名はパスカルケース
- 極力 `public` は使わない．Inspector 上から編集・参照したい場合は `[SerializeField] private` を使う
- `private` な変数には変数名の前に `_`（アンダーバー） をつける
  - `[SerializeField] private` でも同様
- Inspector 上からもいじらない定数には `readonly` をつけて，「大文字 + アンダーバー」で命名する

```C#
private readonly Vector3 _POSITION_A = new Vector3(27.6f, 0.0f, -16.35f);
```

### 入力について

本ゲームでは Input System を利用している．InputSystem はキーボードやパッドなどの入力方法を意識せずに開発出来て便利なので採用してみた．

Input System についての解説記事は下記がわかりやすかった．
基本操作については既に実装しているので，変更を加える必要はないと思うが，気になったら `Script/Player.cs` を参照してください．

[【Unity】Input Action の基本的な使い方 | ねこじゃらシティ](https://nekojara.city/unity-input-system-actions)

### グローバル変数（GV）について

`Script/GV.cs` ではグローバル変数を定義している．グローバル変数はどこからでも呼び出し可能かつゲーム本体に保存される数値となっている．もしも追加で定義したい場合は下記を参照．

```C#
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
```

GV は上記のように定義され，代入時には PlaerPrefs に保存され，参照時には PlayerPrefs から参照される．利用時には特に意識することなく，クラス変数として扱える．

```C#
// 代入
GV.moveSpeed = 10f;

// 参照
Debug.Log(GV.moveSpeed);
```

### 異変について
- 異変は町田教室が生成されるたびにランダムで行なわれるようになっている．
- 町田教室は Player が通路（`Path`）の真ん中まで行くと削除され，反対側に生成される．
- 削除のタイミングですべてリセットされるので，異変で教室をめちゃくちゃにしても問題がない．
- 通路（`Path`）の真ん中にいくと，`_GameManager_`の`GameManagaer`コンポーネントにて審議判定が行なわれる
- 仕様上一度通路（`Path`）から町田教室に出入りする必要があるので，通路（`Path`）正面（異変の数字が見えるところ付近）に異変を置いてはいけない

#### 異変作成のルール

異変の作成に伴い，いくつかのルールがある．Exampleシーンに試作例があるのでご参考までに．

**なお，4/21に仕様を変更しました**

### 最新版
1. 異変に関するものは `Assets/Ihen/Main` に入れよう
2. 異変は，`Assets/Ihen` にある `LitalicoDefo.prefab` を複製して作成しよう（以後「異変ワンダー」と呼称）
3. 異変のスクリプトは必ず `IhenBase` を継承し，`I_XX.cs` という名前にしよう
4. 異変のスクリプトは異変ワンダーにアタッチしよう
5. 異変ワンダーの名前はスクリプト名と同じにしよう
6. 異変ワンダーは `_GameManager_` の `IhenList`コンポーネントの `IhenList` に追加しよう

#### IhenBase とスクリプト例

IhenBase は異変のひな形．これを継承して異変を作成していく．

```C#
// IhenBase.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IhenBase : MonoBehaviour
{
    protected bool _ihenDo = false;
}
```

継承例

```C#
// I_Example.cs
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
        // いらないときは削除推奨
    }
}
```

具体例：色が変わる異変

```C#
// I_BoxColor.cs
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
        _ihenDo = true;
        _targetObject.GetComponent<Renderer>().material.color = color;
    }
}
```

#### dev1以前（Legacy）
**以降に書かれているのは古いバージョンのモノなので無視してOK．**

1. 1 つ 1 つの異変に関連するものは，`Assets/Ihen` に入れること
2. 異変のスクリプトは必ず `IhenBase` を継承し，`Ihen_XX.cs` という名前にすること
3. 異変のスクリプトは必ずゲームオブジェクトにアタッチされ，`Assets/Ihen` 内に Prefab として保存すること
   1. もしも変化するものがデフォルトでは存在しない（もともと無いものが生成される異変など）は空のオブジェクトでも OK
4. 異変 Prefab は `Assets/Ihen/Main/litalicoMain.prefab`の `ihen`（空のオブジェクト）の子オブジェクトとして設置すること
5. 設置した異変 Prefab は `Assets/Ihen/Main/litalicoMain.prefab` の `IhenList` コンポーネントの `IlemList` に登録すること
6. 仕様上一度通路（`Path`）から町田教室に出入りする必要があるので，通路（`Path`）正面（異変の数字が見えるところ付近）に異変を置いてはいけない（教室に入らずに戻ることを防ぐため）
    - 別にバグる分けではないが，正誤判定が行なわれず引き返せなくしている

#### IhenBase とスクリプト例

IhenBase は異変のひな形．これを継承して異変を作成していく．

```C#
// IhenBase.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IhenBase : MonoBehaviour
{
    protected bool _isIhen = false;

    public void SetIhen(bool isIhen)
    {
        _isIhen = isIhen;
        if (isIhen)
        {
            DoIhen();
        }
    }

    abstract protected void DoIhen();
}
```

継承例

```C#
// Ihen_Example.cs
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
```

具体例：色が変わる異変

```C#
// Ihen_BoxColor.cs
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
```
