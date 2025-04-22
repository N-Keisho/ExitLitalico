using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
enum Side
{
    A,
    B
}

public class GameManager : MonoBehaviour
{
    [Header("IhenList")]
    [SerializeField] private IhenList _ihenList;

    [Header("Celling")]
    [SerializeField] private GameObject _CellingA;
    [SerializeField] private GameObject _CellingB;

    [Header("Goal")]
    [SerializeField] private GameObject _goalPath;

    [Header("Test")]
    [SerializeField] private bool _isTest = false;
    [SerializeField] private int _testIndex = 0;

    private readonly Vector3 _POSITION_A = new Vector3(27.6f, 0.0f, -16.35f);
    private readonly Vector3 _POSITION_GOAL_PATH_A = new Vector3(18, 0, -6);
    private GameObject _litalicoObjA;
    private GameObject _litalicoObjB;
    private CurrentNum _currentNum;
    private int _correctNum = 0;
    private Side _currentSide;
    private int _listLen;
    private int _preIhenIndex = 0;
    private bool _isIhen = false;
    private GameInputs _gameInputs;

    void Start()
    {

        InstantLitalico(_ihenList.getDefoLitalico(), Side.A);
        _currentSide = Side.A;
        _CellingA.SetActive(true);
        _CellingB.SetActive(false);
        _listLen = _ihenList.getListLen();

        _gameInputs = new GameInputs();
        _gameInputs.System.Cheat.started += OnCheat;
        _gameInputs.System.Cheat.Enable();
    }

    void OnDestroy()
    {
        _gameInputs.System.Cheat.started -= OnCheat;
        _gameInputs.Disable();
    }

    public void CheckIhen(bool? answerIhen) // ?をつけるとnull許容型になる
    {
        if (answerIhen == null)
        {
            Debug.LogError("Answer is null, skipping Ihen check.");
            return;
        }
        else if (_isIhen == answerIhen)
        {
            _correctNum++;
        }
        else
        {
            _correctNum = 0;
        }

        if (_correctNum >= 8)
        {
            Goal(_currentSide == Side.A ? Side.B : Side.A);
        }
        else
        {
            SwitchLitalico();
        }
    }

    private void SwitchLitalico()
    {
        GameObject _nextLita = null;
        if (_isTest)
        {
            _isIhen = true;
            _nextLita = _ihenList.getIhenLitalico(_testIndex, _isIhen);
            Debug.Log("[Test] Ihen index: " + _testIndex);
        }
        else if (IhenOrNot())
        {
            _isIhen = true;
            _nextLita = RandomIhenGet();
        }
        else
        {
            _isIhen = false;
            _nextLita = _ihenList.getDefoLitalico();
            Debug.Log("Ihen is false, skipping IhenDo.");
        }

        if (_currentSide == Side.A)
        {
            Destroy(_litalicoObjA);
            InstantLitalico(_nextLita, Side.B);
            _currentSide = Side.B;
        }
        else if (_currentSide == Side.B)
        {
            Destroy(_litalicoObjB);
            InstantLitalico(_nextLita, Side.A);
            _currentSide = Side.A;
        }


    }

    private void InstantLitalico(GameObject _nextLita, Side type)
    {
        if (_nextLita == null)
        {
            Debug.LogError("Next Litalico is null, cannot instantiate.");
            return;
        }

        switch (type)
        {
            case Side.A:
                _litalicoObjA = Instantiate(_nextLita, _POSITION_A, Quaternion.Euler(0, 180, 0));
                _litalicoObjA.name = "LitalicoA";
                _currentNum = _litalicoObjA.transform.Find("CurrentNumPannel").GetComponent<CurrentNum>();
                _CellingA.SetActive(true);
                _CellingB.SetActive(false);
                break;

            case Side.B:
                _litalicoObjB = Instantiate(_nextLita, _POSITION_A * -1, Quaternion.identity);
                _litalicoObjB.name = "LitalicoB";
                _currentNum = _litalicoObjB.transform.Find("CurrentNumPannel").GetComponent<CurrentNum>();
                _CellingA.SetActive(false);
                _CellingB.SetActive(true);
                break;
        }

        if (_currentNum == null)
        {
            Debug.LogError("CurrentNum component not found on the object.");
            return;
        }
        _currentNum.SetCurrentNum(_correctNum);
    }

    private bool IhenOrNot()
    {
        return Random.Range(0, 2) == 0;
    }

    private GameObject RandomIhenGet()
    {
        if (_listLen <= 1)
        {
            _isIhen = false;
            Debug.LogError("List length is less than or equal to 1, skipping IhenDo.");
            return _ihenList.getDefoLitalico();
        }

        int index = _preIhenIndex;
        while (index == _preIhenIndex)
        {
            index = Random.Range(0, _listLen);
        }
        _preIhenIndex = index;
        return _ihenList.getIhenLitalico(index, _isIhen);
    }

    private void Goal(Side type)
    {
        switch (type)
        {
            case Side.A:
                Instantiate(_goalPath, _POSITION_GOAL_PATH_A, Quaternion.identity);
                _CellingA.SetActive(true);
                break;
            case Side.B:
                Instantiate(_goalPath, _POSITION_GOAL_PATH_A * -1, Quaternion.Euler(0, 180, 0));
                _CellingB.SetActive(true);
                break;
        }
    }

    private void OnCheat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _correctNum = 6;
            Debug.Log("Cheat activated! Correct number set to 6.");
        }
    }
}
