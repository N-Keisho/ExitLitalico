using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Config : MonoBehaviour
{
    private GameObject _configPanel;
    private GameInputs _gameInputs;
    private bool _isOpen = false;
    void Start()
    {
        _configPanel = this.gameObject;

        _gameInputs = new GameInputs();
        _gameInputs.System.Config.started += OnConfig;
        _gameInputs.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _configPanel.SetActive(false);
    }

    void OnDestroy()
    {
        _gameInputs.Disable();
    }

    private void OnConfig(InputAction.CallbackContext context)
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
}
