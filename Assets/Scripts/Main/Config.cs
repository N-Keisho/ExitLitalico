using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
public class Config : MonoBehaviour
{
    [SerializeField] private GameObject _configPanel;

    [Header("Buttons")]
    [SerializeField] private List<Button> _buttons = new List<Button>();

    [Header("Select")]
    [SerializeField] private GameObject _selectsObj;
    [SerializeField] private GameObject _optionsObj;
    [SerializeField] private GameObject _ihenListObj;

    [Header("Options")]
    [SerializeField] private Player _player;
    [SerializeField] private HeadBobber _headBobber;
    [SerializeField] private Param _sensX;
    [SerializeField] private Param _sensY;
    [SerializeField] private Param _bobber;

    [Header("IhenList")]
    [SerializeField] private IhenList _ihenList;
    [SerializeField] private TMP_Text _ihenListRightText;
    [SerializeField] private TMP_Text _ihenListLeftText;
    [SerializeField] private TMP_Text _ihenListUnknownText;

    [Header("Fade")]
    [SerializeField] private MainFade _mainFade;

    private bool _isOpen = false;
    private bool _isClear = false;
    private bool _isFade = false;
    private int _unknownCount = 0;
    private int _currentSelect = 2;
    public event Action<bool> OnConfigStateChanged;
    public event Action OnConfigSoundPlayed;

    void Start()
    {
        _isClear = GV.isClear;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _sensX.SetValue(GV.sensX_buf);
        _sensY.SetValue(GV.sensY_buf);
        _bobber.SetValue(GV.bobber_buf);

        _ihenListRightText.text = "";
        _ihenListLeftText.text = "";

        if (_isClear)
        {
            _buttons[3].interactable = true;
            IhenListUpdate();
        }
        else
        {
            _buttons[3].GetComponentInChildren<TMP_Text>().text = "???";
        }

        _configPanel.SetActive(false);
    }

    // --- Input系の関数群 ---
    public void OnConfig(InputAction.CallbackContext context)
    {
        ShowConfig();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (!_isOpen) return;
        _buttons[_currentSelect].onClick.Invoke();
        OnConfigSoundPlayed?.Invoke();
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (!_isOpen) return;
        Back();
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (!_isOpen) return;

        _currentSelect--;

        if (_currentSelect < 0)
        {
            _currentSelect = _buttons.Count - 1;
        }
        if (_currentSelect == 3 && !_isClear)
        {
            _currentSelect = 2;
        }

        _buttons[_currentSelect].Select();
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (!_isOpen) return;

        _currentSelect++;

        if (_currentSelect > _buttons.Count - 1)
        {
            _currentSelect = 0;
        }
        if (_currentSelect == 3 && !_isClear)
        {
            _currentSelect = 4;
        }

        _buttons[_currentSelect].Select();
    }


    // --- Select系の関数群 ---
    public void ShowConfig()
    {

        _isOpen = !_isOpen;
        if (_isOpen)
        {
            Time.timeScale = 0f;

            _configPanel.SetActive(true);
            _selectsObj.SetActive(true);
            _optionsObj.SetActive(false);
            _ihenListObj.SetActive(false);

            _currentSelect = 2;
            _buttons[_currentSelect].Select();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;

            _configPanel.SetActive(false);
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        OnConfigStateChanged?.Invoke(_isOpen);
    }

    public void ShowOptions()
    {
        _selectsObj.SetActive(false);
        _optionsObj.SetActive(true);
        _ihenListObj.SetActive(false);
    }

    public void ShowIhenList()
    {
        _selectsObj.SetActive(false);
        _optionsObj.SetActive(false);
        _ihenListObj.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }

    public void ToTitle()
    {
        Debug.Log("ToTitle");
        if (_isFade) return;
        _isFade = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0.01f;
        _mainFade.FadeOut("Title", 0.01f);
    }

    public void Back()
    {
        _selectsObj.SetActive(true);
        _optionsObj.SetActive(false);
        _ihenListObj.SetActive(false);
    }

    // --- Optionsの値を変更する関数群 ---
    public void OnSensXChange()
    {
        float value = _sensX.value;
        GV.sensX_buf = value;
        _player.SetSensX(value);
    }
    public void OnSensYChange()
    {
        float value = _sensY.value;
        GV.sensY_buf = value;
        _player.SetSensY(value);
    }

    public void OnBobberChange()
    {
        float value = _bobber.value;
        GV.bobber_buf = value;
        _headBobber.SetBobber(value);
    }

    // --- IhenListの更新 ---
    private void IhenListUpdate()
    {
        string text = "";
        int len = _ihenList.getListLen();
        for (int i = 0; i < len; i++)
        {
            IhenBase ihen = _ihenList.getIhen(i);
            if (GV.IsDoneIhen(ihen.name))
            {
                text = ihen.Explanation + "\n";
            }
            else
            {
                text = "???" + "\n";
                _unknownCount++;
            }

            if (i < len / 2)
            {
                _ihenListLeftText.text += text;
            }
            else
            {
                _ihenListRightText.text += text;
            }
        }

        if (_unknownCount > 0)
        {
            _ihenListUnknownText.text = "未発見数　" + _unknownCount;
        }
    }
}
