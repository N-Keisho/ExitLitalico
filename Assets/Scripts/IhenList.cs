using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhenList : MonoBehaviour
{
    [SerializeField] private GameObject _litalicoDefo;
    [SerializeField] private List<GameObject> _ihenList = new List<GameObject>();

    private void Awake()
    {
        if (_litalicoDefo == null)
        {
            Debug.LogError("LitalicoDefo is not assigned in the inspector.");
        }
        else
        {
            CheckIhenList();
        }
    }

    private void CheckIhenList()
    {
        if (_ihenList == null || _ihenList.Count == 0)
        {
            Debug.LogError("IhenList is not initialized or empty.");
        }
        else
        {
            foreach (GameObject item in _ihenList)
            {
                IhenBase ihenBase = item.GetComponent<IhenBase>();
                if (ihenBase == null)
                {
                    Debug.LogError("IhenList contains an item without IhenBase component: " + item.name);
                }
            }
        }
    }
    public int getListLen()
    {
        return _ihenList.Count;
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

        return _ihenList[index];
    }
}
