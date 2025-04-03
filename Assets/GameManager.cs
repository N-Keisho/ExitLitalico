using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Type
{
    A,
    B
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject LitalicoPrefab;
    private readonly Vector3 _POSITION_A = new Vector3(27.6f, 0.0f, -16.35f);
    private GameObject _litalicoObjA;
    private GameObject _litalicoObjB;
    private Type _currentType;
    private Litalico _litalico;
    private int _listLen;
    private int _preIhenIndex = 0;
    private bool _isIhen = false;
    private GameInputs _gameInputs;

    void Start()
    {
        _gameInputs = new GameInputs();
        _gameInputs.Test.Space.started += ctx => SwitchLitalico();
        _gameInputs.Test.Enable();

        InstantLitalico(Type.A);
        _currentType = Type.A;
        _listLen = _litalico.getListLen();
    }

    private void OnDestroy()
    {
        _gameInputs.Test.Space.started -= ctx => SwitchLitalico();
        _gameInputs.Test.Disable();
    }

    public void SwitchLitalico()
    {
        if (_currentType == Type.A)
        {
            Destroy(_litalicoObjA);
            InstantLitalico(Type.B);
            _currentType = Type.B;
        }
        else if (_currentType == Type.B)
        {
            Destroy(_litalicoObjB);
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
                _litalicoObjA = Instantiate(LitalicoPrefab, _POSITION_A, Quaternion.Euler(0, 180, 0));
                _litalicoObjA.name = "LitalicoA";
                _litalico = _litalicoObjA.GetComponent<Litalico>();
                break;
            case Type.B:
                _litalicoObjB = Instantiate(LitalicoPrefab, _POSITION_A * -1, Quaternion.identity);
                _litalicoObjB.name = "LitalicoB";
                _litalico = _litalicoObjB.GetComponent<Litalico>();
                break;
        }

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
        if(_listLen <= 1)
        {
            Debug.LogError("List length is less than or equal to 1, skipping IhenDo.");
            return;
        }

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
