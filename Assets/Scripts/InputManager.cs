using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameInputs _gameInputs;
    
    [Header("GameObjects")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private Config _config;
    [SerializeField] private IhenList _ihenList;

    [Header("Parameters")]
    [SerializeField] private float _waitTime = 1f; // 待機時間
    void Start()
    {
        _gameInputs = new GameInputs();

        _gameInputs.Player.Move.started += _player.OnMove;
        _gameInputs.Player.Move.performed += _player.OnMove;
        _gameInputs.Player.Move.canceled += _player.OnMove;

        _gameInputs.Player.Look.started += _player.OnLook;
        _gameInputs.Player.Look.performed += _player.OnLook;
        _gameInputs.Player.Look.canceled += _player.OnLook;

        _gameInputs.Player.Dash.started += _player.OnDash;
        _gameInputs.Player.Dash.canceled += _player.OnDash;

        _gameInputs.System.Cheat.started += _gameManager.OnCheat;

        _gameInputs.System.Config.started += _config.OnConfig;

        _gameInputs.System.GameQuit.started += _config.OnGameQuit;

        _gameInputs.System.ResetIhenDone.started += _ihenList.ResetIhenDone;

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
}
