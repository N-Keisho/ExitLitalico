using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum Side
    {
        A,
        B
    }


    [Header("GameObjects")]
    [SerializeField] private IhenList _ihenList;
    [SerializeField] private Path _path;

    [Header("Celling")]
    [SerializeField] private GameObject _CellingA;
    [SerializeField] private GameObject _CellingB;

    [Header("StaticLitalico")]
    [SerializeField] private GameObject _staticLitalicoA;
    [SerializeField] private GameObject _staticLitalicoB;

    [Header("Goal")]
    [SerializeField] private GameObject _goalPath;

    [Header("Test")]
    [SerializeField] private bool _isTest = false;
    [SerializeField] private int _testIndex = 0;
    [SerializeField] private bool _ihenOnly = false;

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
    private bool _isClear = false;

    void Start()
    {
        InstantLitalico(_ihenList.getDefoLitalico(), Side.A);
        _currentSide = Side.A;
        _CellingA.SetActive(true);
        _staticLitalicoA.SetActive(true);
        _CellingB.SetActive(false);
        _staticLitalicoB.SetActive(false);
        _listLen = _ihenList.getListLen();

    }

    public void CheckIhen(bool? answerIhen) // ?をつけるとnull許容型になる
    {
        if (_isClear) return; // クリア済みなら何もしない
        else if (answerIhen == null)
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
        else if (_ihenOnly)
        {
            _isIhen = true;
            _nextLita = RandomIhenGet();
            Debug.Log("[IhenOnly]");
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
                _staticLitalicoA.SetActive(true);
                _CellingB.SetActive(false);
                _staticLitalicoB.SetActive(false);
                break;

            case Side.B:
                _litalicoObjB = Instantiate(_nextLita, _POSITION_A * -1, Quaternion.identity);
                _litalicoObjB.name = "LitalicoB";
                _currentNum = _litalicoObjB.transform.Find("CurrentNumPannel").GetComponent<CurrentNum>();
                _CellingA.SetActive(false);
                _staticLitalicoA.SetActive(false);
                _CellingB.SetActive(true);
                _staticLitalicoB.SetActive(true);
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
        int _randomNum = Random.Range(0, 5);
        if (_randomNum <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject RandomIhenGet()
    {
        if (_listLen <= 1)
        {
            _isIhen = false;
            Debug.LogError("List length is less than or equal to 1, skipping IhenDo.");
            return _ihenList.getDefoLitalico();
        }

        // まだ実行されていない異変のインデックス
        List<int> _notDoneIhenIndexes = _ihenList.getNotDoneIhenIndexes();

        if (_notDoneIhenIndexes.Count == 0)
        {
            // 0個のときは，ランダムな異変
            int index = _preIhenIndex;
            while (index == _preIhenIndex)
            {
                index = Random.Range(0, _listLen);
            }
            _preIhenIndex = index;
            return _ihenList.getIhenLitalico(index, _isIhen);
        }
        else
        {
            // 3回に1回の確率でランダムな異変を選ぶ
            if (Random.Range(0, 3) == 0)
            {
                int index = _preIhenIndex;
                while (index == _preIhenIndex)
                {
                    index = Random.Range(0, _listLen);
                }
                _preIhenIndex = index;
                return _ihenList.getIhenLitalico(index, _isIhen);
            }
            else
            {
                // まだ実行されていない異変のインデックスからランダムに選ぶ
                int index = Random.Range(0, _notDoneIhenIndexes.Count);
                _preIhenIndex = _notDoneIhenIndexes[index];
                return _ihenList.getIhenLitalico(_preIhenIndex, _isIhen);
            }
        }
    }

    private void Goal(Side type)
    {
        _isClear = true;
        _path.Clear(type);
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

    public void OnCheat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _correctNum = 6;
            Debug.Log("Cheat activated! Correct number set to 6.");
        }
    }
}
