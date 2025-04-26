using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "IhenList", menuName = "IhenList", order = 0)]
public class IhenList : ScriptableObject
{
    [SerializeField] private GameObject _litalicoDefo;
    [SerializeField] private List<IhenBase> _ihenList = new List<IhenBase>();

    public int getListLen()
    {
        return _ihenList.Count;
    }

    public IhenBase getIhen(int index)
    {
        if (_ihenList == null || _ihenList.Count == 0)
        {
            Debug.LogError("IhenList is not initialized or empty.");
            return null;
        }
        else if (index < 0 || index >= _ihenList.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return null;
        }

        return _ihenList[index];
    }

    public GameObject getDefoLitalico()
    {
        return _litalicoDefo;
    }
    
    public GameObject getIhenLitalico(int index, bool isIhen)
    {
        if (isIhen == false)
        {
            Debug.Log("Ihen is false.");
            return _litalicoDefo;
        }
        else if (_ihenList == null || _ihenList.Count == 0)
        {
            Debug.LogError("IhenList is not initialized or empty.");
            return _litalicoDefo;
        }
        else if (index < 0 || index >= _ihenList.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return _litalicoDefo;
        }

        return _ihenList[index].gameObject;
    }

    public void ResetIhenDone(InputAction.CallbackContext context)
    {
        foreach (IhenBase item in _ihenList)
        {
            Debug.Log("Resetting Ihen: " + item.gameObject.name);
            GV.SetDoneIhen(item.gameObject.name, false);
        }
    }
}
