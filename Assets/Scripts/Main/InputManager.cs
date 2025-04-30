using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private GameInputs _gameInputs;

    [Header("GameObjects")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private Config _config;
    [SerializeField] private IhenList _ihenList;
    [SerializeField] private CameraZoom _cameraZoom;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _sysSound;
    [SerializeField] private AudioClip _configSound;

    [Header("Parameters")]
    [SerializeField] private float _waitTime = 1f; // 待機時間

    private bool _isConfigOpen = false;
    void Start()
    {
        _gameInputs = new GameInputs();

        // Playerの動作
        _gameInputs.Player.Move.started += _player.OnMove;
        _gameInputs.Player.Move.performed += _player.OnMove;
        _gameInputs.Player.Move.canceled += _player.OnMove;

        _gameInputs.Player.Look.started += _player.OnLook;
        _gameInputs.Player.Look.performed += _player.OnLook;
        _gameInputs.Player.Look.canceled += _player.OnLook;

        _gameInputs.Player.Dash.started += _player.OnDash;
        _gameInputs.Player.Dash.canceled += _player.OnDash;

        _gameInputs.Player.Zoom.started += _cameraZoom.OnZoom;
        _gameInputs.Player.Zoom.canceled += _cameraZoom.OnZoom;

        // 秘密コマンド
        _gameInputs.System.Cheat.started += _gameManager.OnCheat;
        _gameInputs.System.Cheat.started += PlaySysSound;

        _gameInputs.System.ResetIhenDone.started += _ihenList.ResetIhenDone;
        _gameInputs.System.ResetIhenDone.started += PlaySysSound;

        // Config周り
        _gameInputs.System.Config.started += _config.OnConfig;
        _gameInputs.System.Config.started += PlayConfigSound;
        _gameInputs.System.Config.started += OnConfig;

        _gameInputs.UI.Select.started += _config.OnSelect;
        _gameInputs.UI.Up.started += _config.OnUp;
        _gameInputs.UI.Down.started += _config.OnDown;
        _gameInputs.UI.Back.started += _config.OnBack;

        Invoke("Enabled", _waitTime);
    }

    private void Enabled()
    {
        _gameInputs.Enable();
    }

    public void Dispose()
    {
        _gameInputs?.Dispose();
    }

    private void PlaySysSound(InputAction.CallbackContext context)
    {
        if (_audioSource != null && _sysSound != null)
        {
            _audioSource.PlayOneShot(_sysSound);
        }
    }

    private void PlayConfigSound(InputAction.CallbackContext context)
    {
        if (_audioSource != null && _config != null)
        {
            _audioSource.PlayOneShot(_configSound);
        }
    }

    private void OnConfig(InputAction.CallbackContext context)
    {
        if (_isConfigOpen)
        {
            _isConfigOpen = false;
            _gameInputs.Player.Enable();
        }
        else
        {
            _isConfigOpen = true;
            _gameInputs.Player.Disable();
        }
    }
}
