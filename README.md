# ワンダー町田から脱出せよ！
![スクリーンショット 2025-05-06 153520](https://github.com/user-attachments/assets/c62c1a98-7186-4090-a820-794302892e20)

ごにきが作ってくれたワンダー町田のモデルを基に開発した「8 番出口」ライクなゲームです．

- Windows版（推奨）：https://github.com/N-Keisho/ExitLitalico/releases
- Web版：https://unityroom.com/games/exitlitalico

※Windows版は「Build.zip」のみダウンロード -> 解凍して「ExitLitalico.exe」を実行　で遊べます！

※重たいのでWindows版推奨です．Web版でも遊べますが，カクつくかも...．

## ゲーム説明
あなたはLITALICOワンダー町田教室に囚われてしまった。
異変があるときは引き返し、ない時は先に進んで、おうちに帰ろう！

### ルール
- 異変があったら引き返そう！
- 異変がなかったら先に進もう！
- この教室からおうちに帰ろう！

### 注意事項
- LITALICOの文字がすべて消えているときは異変がないので，よく見て覚えてね！

### 基本操作
- 移動：WASD or 左スティック
- 視点：マウス移動 or 右スティック
- ダッシュ：Shift or （左or右）トリガー（R2L2）
- ズーム：Ctrl or マウス中央押し込み or （左or右）ショルダー（R1L1）
- ポーズ画面：Tab or Homeボタン or Startボタン or Selectボタン
- ゲーム終了：ポーズ画面でから選択 or タイトル画面でEsc

### 隠しコマンド（音が鳴る）
- 異変発見チート：「Shift + Ctrl + Tab」 or 「（左or右）トリガー + （左or右）ショルダー + Startボタン」
    - 正解数が6回になる
- 異変発見状態リセット：backspace + delete

### 異変一覧
<details><summary>重大なネタバレを含みます．ご注意ください．</summary>

#### 本採用
1. 3Dプリンターが爆発
2. 空気清浄機から煙
3. 全てのPCがMac
4. ポスターが降ってくる
5. 魚がタイムアタックしている
6. 天井に人の顔がある
7. 真ん中の棚がない
8. ライトが徐々に赤くなる
9. クッションが積みあがる
10. 偽物の出口
11. だおだおが追いかけてくる
12. 窓の外から何かが見ている
13. カーテンが落ちる
14. ゲーミングパン
15. 物置に誰かが閉じ込められている
16. レゴの箱が整理されている
17. 防犯カメラが増えている
18. 壁一面にポスター
19. ほとんどなにもない
20. 机の配置が宴仕様
21. PCがだんだん大きくなる
22. りりーのダンスタイム
23. モニターに砂嵐
24. 靴箱の段数が多い
25. トイレのドアが開いている
26. モニターがプレイヤーを向く
27. トイレへの道が封鎖されている
28. ウォーターサーバから水
29. ホワイトボードに町田のみんな
30. ホワイトボードにひきかえせ

#### 没
- プリンターがない
- サイドの机が四角
 
#### 更新
- 動画の再生速度が遅い→モニターに砂嵐
- クッションが緑一色→クッションが積みあがる
- レゴの箱が全部黒 → レゴの箱が整理されている
</details>



## 開発メモ
<details><summary>開発時に使用したメモです</summary>

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
Logger.Log(GV.moveSpeed);
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

### 最新版：4/27 Update
1. 異変に関するものは `Assets/Ihen/Main` に入れよう
2. 異変は，`Assets/Ihen` にある `LitalicoDefo.prefab` を `右クリック > Create > Prefab Variant` で作成しよう（以後「異変ワンダー」と呼称）
3. 異変のスクリプトは必ず `IhenBase` を継承し，`I_XX.cs` という名前にしよう
4. 異変のスクリプトは異変ワンダーにアタッチしよう
5. 異変ワンダーの名前はスクリプト名と同じにしよう
6. 異変ワンダーは `Assets/Ihen` にある　`Main`（ScriptableObject）の `IhenList` に追加しよう
7. 容量削減のため，異変ワンダーの'WillDelete'は削除しよう

**※注意**　Prefab Variantで作成しないと，`LitalicoDefo.prefab` の変更が適応されなくなるので気を付けて！

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
        Logger.Log("Ihen_Example: 異変が起きました！");
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
<details><summary>以降に書かれているのは古いバージョンのモノなので無視してOK．</summary>

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
</details>
</details>
