using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
// 消えたり出たりする異変の例
// </summary>
public class I_BoxAppearance : IhenBase
{
    [SerializeField] private GameObject _targetObject; // 対象のオブジェクト
    void Start()
    {
        _ihenDo = true; // 異変が起きている状態にする
        _targetObject.SetActive(true);
    }
}
