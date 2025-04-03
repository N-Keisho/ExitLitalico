using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Side
{
    A,
    B
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject LitalicoPrefab;
    [SerializeField] private int correctNum = 0;
    private readonly Vector3 _POSITION_A = new Vector3(27.6f, 0.0f, -16.35f);
    private GameObject _litalicoObjA;
    private GameObject _litalicoObjB;
    private Side _currentSide;
    private IhenList _ihenList;
    private int _listLen;
    private int _preIhenIndex = 0;
    private bool _isIhen = false;
    private GameInputs _gameInputs;

    void Start()
    {
        InstantLitalico(Side.A);
        _currentSide = Side.A;
        _listLen = _ihenList.getListLen();
    }

    public void CheckIhen(bool? answerIhen) // ?をつけるとnull許容型になる
    {
        if (answerIhen == null)
        {
            Debug.LogError("Answer is null, skipping Ihen check.");
            return;
        }
        else if(_isIhen == answerIhen)
        {
            correctNum++;
            Debug.Log("answer is correct.");
        }
        else
        {
            correctNum = 0;
            Debug.Log("answer is incorrect.");
        }
        SwitchLitalico();
    }


    private void SwitchLitalico()
    {
        if (_currentSide == Side.A)
        {
            Destroy(_litalicoObjA);
            InstantLitalico(Side.B);
            _currentSide = Side.B;
        }
        else if (_currentSide == Side.B)
        {
            Destroy(_litalicoObjB);
            InstantLitalico(Side.A);
            _currentSide = Side.A;
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

    private void InstantLitalico(Side type)
    {
        switch (type)
        {
            case Side.A:
                _litalicoObjA = Instantiate(LitalicoPrefab, _POSITION_A, Quaternion.Euler(0, 180, 0));
                _litalicoObjA.name = "LitalicoA";
                _ihenList = _litalicoObjA.GetComponent<IhenList>();
                break;
            case Side.B:
                _litalicoObjB = Instantiate(LitalicoPrefab, _POSITION_A * -1, Quaternion.identity);
                _litalicoObjB.name = "LitalicoB";
                _ihenList = _litalicoObjB.GetComponent<IhenList>();
                break;
        }

        if (_ihenList == null)
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
        _ihenList.DoIhen(index, true);
        _preIhenIndex = index;
    }
}
