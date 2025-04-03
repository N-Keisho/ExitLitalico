using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _verRot;
    [SerializeField] private Path _path;
    private GameInputs _gameInputs;
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    private bool _isDash = false;  
    private string _preDetectTag = "";
    private float _moveSpeed;
    private float _dashSpeed;
    private float _sensX;
    private float _sensY;
    private float _minY;
    private float _maxY;
    private Vector2 _moveInputValue;
    private Vector2 _rotateInputValue;
    private Vector3 _direction;

    void Start()
    {
        _gameInputs = new GameInputs();

        _gameInputs.Player.Move.started += OnMove;
        _gameInputs.Player.Move.performed += OnMove;
        _gameInputs.Player.Move.canceled += OnMove;

        _gameInputs.Player.Look.started += OnLook;
        _gameInputs.Player.Look.performed += OnLook;
        _gameInputs.Player.Look.canceled += OnLook;

        _gameInputs.Player.Dash.started += OnDash;
        _gameInputs.Player.Dash.canceled += OnDash;

        _gameInputs.Enable();

        // GVから値を取得
        _moveSpeed = GV.moveSpeed;
        _dashSpeed = GV.dashSpeed;
        _sensX = GV.sensX;
        _sensY = GV.sensY;
        _minY = GV.minY;
        _maxY = GV.maxY;
    }

    void Update()
    {
        Move();
        Look();
    }

    private void OnDestroy()
    {
        _gameInputs.Disable();
    }

    // --- プレイヤーの行動 ---
    private void Move()
    {
        // Rayの開始位置と方向を設定
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = _direction;

        // Rayを飛ばして、障害物に当たったかどうかをチェック
        RaycastHit hit;
        float rayDistance = _moveSpeed + 0.1f; // 少し余裕を持たせるために0.1fを追加
        bool isHit = Physics.Raycast(rayStart, rayDirection, out hit, 0.7f);
        if (!isHit || hit.collider.tag.Contains("Gate"))
        {
            // Rayが何もヒットしない場合、プレイヤーを移動
            if (_isDash)
            {
                transform.position += _direction * _dashSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += _direction * _moveSpeed * Time.deltaTime;
            }
        }
        Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.red); // Rayの可視化
    }

    private void Look()
    {
        // Y軸の回転を計算
        _rotationX += _rotateInputValue.x * _sensX * Time.deltaTime;
        _rotationY -= _rotateInputValue.y * _sensY * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, _minY, _maxY);

        // プレイヤーの回転を適用
        transform.localRotation = Quaternion.Euler(0, _rotationX, 0);

        // カメラの回転を適用
        _verRot.transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);
    }

    // --- Input System からの入力を受け取るメソッド ---
    // 発火のタイミングは、Input System の設定に依存するので，値の更新だけ行う
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInputValue = context.ReadValue<Vector2>();
        _direction = (transform.forward * _moveInputValue.y + transform.right * _moveInputValue.x).normalized;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        _rotateInputValue = context.ReadValue<Vector2>();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isDash = true;
        }
        else if (context.canceled)
        {
            _isDash = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Gate") && _preDetectTag != other.tag)
        {
            if(other.gameObject.CompareTag("GateA1in"))
            {
                if(_preDetectTag.Equals("GateA1out"))
                {
                    Debug.Log("GateA1から入った");
                    _path.InA1();
                }
            }
            else if(other.gameObject.CompareTag("GateA1out"))
            {
                if(_preDetectTag.Equals("GateA1in"))
                {
                    Debug.Log("GateA1から出た");
                    _path.Out();
                }
            }
            else if(other.gameObject.CompareTag("GateA2in"))
            {
                if(_preDetectTag.Equals("GateA2out"))
                {
                    Debug.Log("GateA2から入った");
                    _path.InA2();
                }
            }
            else if(other.gameObject.CompareTag("GateA2out"))
            {
                if(_preDetectTag.Equals("GateA2in"))
                {
                    Debug.Log("GateA2から出た");
                    _path.Out();
                }
            }
            else if(other.gameObject.CompareTag("GateB1in"))
            {
                if(_preDetectTag.Equals("GateB1out"))
                {
                    Debug.Log("GateB1から入った");
                    _path.InB1();
                }
            }
            else if(other.gameObject.CompareTag("GateB1out"))
            {
                if(_preDetectTag.Equals("GateB1in"))
                {
                    Debug.Log("GateB1から出た");
                    _path.Out();
                }
            }
            else if(other.gameObject.CompareTag("GateB2in"))
            {
                if(_preDetectTag.Equals("GateB2out"))
                {
                    Debug.Log("GateB2から入った");
                    _path.InB2();
                }
            }
            else if(other.gameObject.CompareTag("GateB2out"))
            {
                if(_preDetectTag.Equals("GateB2in"))
                {
                    Debug.Log("GateB2から出た");
                    _path.Out();
                }
            }
            _preDetectTag = other.tag;
        }
    }
}
