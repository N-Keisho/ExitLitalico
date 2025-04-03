using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhenList : MonoBehaviour
{
    [SerializeField] private List<IhenBase> _ihenList = new List<IhenBase>();
    public int getListLen()
    {
        return _ihenList.Count;
    }
    public void DoIhen(int index, bool isIhen)
    {
        if(isIhen == false)
        {
            Debug.Log("Ihen is false, skipping DoIhen.");
            return;
        }
        else if (_ihenList == null || _ihenList.Count == 0)
        {
            Debug.LogError("IhenList is not initialized or empty.");
            return;
        }
        else if (index < 0 || index >= _ihenList.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return;
        }

        IhenBase ihen = _ihenList[index].GetComponent<IhenBase>();
        if (ihen == null)
        {
            Debug.LogError("IhenBase component not found on the object at index: " + index);
            return;
        }
        ihen.SetIhen(isIhen);
    }
}
