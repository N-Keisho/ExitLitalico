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
