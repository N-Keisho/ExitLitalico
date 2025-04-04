using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
// 消えたり出たりする異変の例
// </summary>
public class Ihen_BoxAppearance : IhenBase
{
    protected override void DoIhen()
    {
        this.gameObject.SetActive(true);
    }
}
