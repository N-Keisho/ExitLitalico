using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    A,
    B
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject LitalicoPrefab;
    private readonly Vector3 _POSITION_A = new Vector3(27.6f, 0.0f, -16.35f);
    private GameObject _litalicoObj;
    private Type _currentType;
    private Litalico _litalico;
    private int _listLen;
    private int _preIhenIndex = 0;
    private bool _isIhen = false;

    void Start()
    {
        InstantLitalico(Type.A);
        _currentType = Type.A;
        _listLen = _litalico.getListLen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchLitalico();
        }
    }

    public void SwitchLitalico()
    {
        if (_currentType == Type.A)
        {
            Destroy(_litalicoObj);
            InstantLitalico(Type.B);
            _currentType = Type.B;
        }
        else if (_currentType == Type.B)
        {
            Destroy(_litalicoObj);
            InstantLitalico(Type.A);
            _currentType = Type.A;
        }

        if(IhenOrNot())
        {
            _isIhen = true;
            RandomIhenDo();
        }
        else
        {
            _isIhen = false;
            Debug.Log("Ihen is false, skipping IhenDo.");
        }
    }

    private void InstantLitalico(Type type)
    {
        switch (type)
        {
            case Type.A:
                _litalicoObj = Instantiate(LitalicoPrefab, _POSITION_A, new Quaternion(0, 180, 0, 0));
                _litalicoObj.name = "LitalicoA";
                break;
            case Type.B:
                _litalicoObj = Instantiate(LitalicoPrefab, _POSITION_A * -1, Quaternion.identity);
                _litalicoObj.name = "LitalicoB";
                break;
        }

        _litalico = _litalicoObj.GetComponent<Litalico>();
        if (_litalico == null)
        {
            Debug.LogError("Litalico component not found on the object.");
            return;
        }
    }

    private bool IhenOrNot()
    {
        return Random.Range(0, 2) == 0;
    }

    private void RandomIhenDo()
    {
        int index = _preIhenIndex;
        while (index == _preIhenIndex)
        {
            index = Random.Range(0, _listLen);
        }
        Debug.Log("Ihen index: " + index);
        _litalico.DoIhen(index, true);
        _preIhenIndex = index;
    }
}
