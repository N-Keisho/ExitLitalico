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
            Logger.Error("IhenList is not initialized or empty.");
            return null;
        }
        else if (index < 0 || index >= _ihenList.Count)
        {
            Logger.Error("Index out of range: " + index);
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
            Logger.Log("Ihen is false.");
            return _litalicoDefo;
        }
        else if (_ihenList == null || _ihenList.Count == 0)
        {
            Logger.Error("IhenList is not initialized or empty.");
            return _litalicoDefo;
        }
        else if (index < 0 || index >= _ihenList.Count)
        {
            Logger.Error("Index out of range: " + index);
            return _litalicoDefo;
        }

        return _ihenList[index].gameObject;
    }

    public void ResetIhenDone(InputAction.CallbackContext context)
    {
        foreach (IhenBase item in _ihenList)
        {
            GV.SetDoneIhen(item.gameObject.name, false);
        }
        GV.isClear = false;
        Logger.Log("IhenList reset done status.");
    }

    public List<int> getNotDoneIhenIndexes()
    {
        List<int> notDoneIndexes = new List<int>();
        for (int i = 0; i < _ihenList.Count; i++)
        {
            if (!GV.IsDoneIhen(_ihenList[i].gameObject.name))
            {
                notDoneIndexes.Add(i);
            }
        }
        return notDoneIndexes;
    }
}
