using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
// 消えたり出たりする異変の例
// </summary>
public class I_BoxAppearance : IhenBase
{
    [SerializeField] private GameObject _targetObject;
    void Start()
    {
        _targetObject.SetActive(true);
    }
}
