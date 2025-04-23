using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Config : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HeadBobber _headBobber;
    [SerializeField] private Param _sensX;
    [SerializeField] private Param _sensY;
    [SerializeField] private Param _bobber;

    private GameObject _configPanel;
    private bool _isOpen = false;
    void Start()
    {
        _configPanel = this.gameObject;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _configPanel.SetActive(false);
    }

    public void OnConfig(InputAction.CallbackContext context)
    {
        _isOpen = !_isOpen;
        if (_isOpen)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _configPanel.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _configPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

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
}
