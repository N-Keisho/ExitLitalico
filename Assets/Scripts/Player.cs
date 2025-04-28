using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _verRot;
    [SerializeField] private Path _path;
    [SerializeField] private GameManager _gameManager;
    private readonly float _MOVE_SPEED = 5f;
    private readonly float _DASH_SPEED = 10f;
    private readonly float _MIN_Y = -60f;
    private readonly float _MAX_Y = 60f;
    private readonly float _SENS_X = 150f;
    private readonly float _SENS_Y = 150f;
    private Vector2 _moveInputValue;
    private Vector2 _rotateInputValue;
    private Vector3 _direction;
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    private float _sensX_buf;
    private float _sensY_buf;
    private string _preDetectTag = "";
    private bool _isMove = false;
    private bool _isDash = false;
    private bool _isCurpet = false;
    private bool? _answer = null;       // true: 異変アリ(1)， false: 異変ナシ(2), null: 未回答
    public bool IsMove { get { return _isMove; } }   // 読み取り専用プロパティ
    public bool IsDash { get { return _isDash; } }   // 読み取り専用プロパティ
    public bool IsCurpet { get { return _isCurpet; } }   // 読み取り専用プロパティ

    void Start()
    {

        // GVから値を取得
        _sensX_buf = GV.sensX_buf;
        _sensY_buf = GV.sensY_buf;

        // 初期の向きを設定
        _rotationX = transform.localRotation.eulerAngles.y;
        _rotationY = _verRot.transform.localRotation.eulerAngles.x;
    }

    void Update()
    {
        _isMove = (_moveInputValue != Vector2.zero);
        if (_isMove) Move();
        Look();
        if(transform.position.y < -10) SceneManager.LoadScene("Main");
    }

    // --- プレイヤーの行動 ---
    private void Move()
    {
        // Rayの開始位置と方向を設定
        Vector3 rayStart = transform.position - new Vector3(0, 1f, 0);
        Vector3 rayDirection = _direction;

        // Rayを飛ばして、障害物に当たったかどうかをチェック
        RaycastHit hit;
        float rayDistance = _MOVE_SPEED + 0.1f; // 少し余裕を持たせるために0.1fを追加
        bool isHit = Physics.Raycast(rayStart, rayDirection, out hit, 1f);
        if (!isHit || hit.collider.tag.Contains("Gate"))
        {

            // Rayが何もヒットしない場合、プレイヤーを移動
            if (_isDash)
            {
                transform.position += _direction * _DASH_SPEED * Time.deltaTime;
            }
            else
            {
                transform.position += _direction * _MOVE_SPEED * Time.deltaTime;
            }
        }
        else
        {
            _isMove = false;
        }
        Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.red); // Rayの可視化
    }

    private void Look()
    {
        // Y軸の回転を計算
        _rotationX += _rotateInputValue.x * _SENS_X * _sensX_buf * Time.deltaTime;
        _rotationY -= _rotateInputValue.y * _SENS_Y * _sensY_buf * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, _MIN_Y, _MAX_Y);

        // プレイヤーの回転を適用
        transform.localRotation = Quaternion.Euler(0, _rotationX, 0);

        // カメラの回転を適用
        _verRot.transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);
    }

    // --- Input System からの入力を受け取るメソッド ---
    // 発火のタイミングは、Input System の設定に依存するので，値の更新だけ行う
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInputValue = context.ReadValue<Vector2>();
        _direction = (transform.forward * _moveInputValue.y + transform.right * _moveInputValue.x).normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _rotateInputValue = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
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

    // --- 感度の変更 ---
    public void SetSensX(float value)
    {
        _sensX_buf = value;
    }
    public void SetSensY(float value)
    {
        _sensY_buf = value;
    }

    // --- ゲートの判定 ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Gate") && _preDetectTag != other.tag)
        {
            if (other.gameObject.CompareTag("GateCheckA") && _answer != null)
            {
                if (_preDetectTag.Equals("GateCheckB"))
                {
                    // Debug.Log("判定B");
                    _path.CheckB();
                    _gameManager.CheckIhen(_answer);
                    _answer = null;

                }
            }
            else if (other.gameObject.CompareTag("GateCheckB") && _answer != null)
            {
                if (_preDetectTag.Equals("GateCheckA"))
                {
                    // Debug.Log("判定A");
                    _path.CheckA();
                    _gameManager.CheckIhen(_answer);
                    _answer = null;
                }
            }
            else if (other.gameObject.CompareTag("GateA1in"))
            {
                if (_preDetectTag.Equals("GateA1out"))
                {
                    // Debug.Log("GateA1から入った");
                    _path.InA1();
                    _answer = true;
                }
            }
            else if (other.gameObject.CompareTag("GateA1out"))
            {
                if (_preDetectTag.Equals("GateA1in"))
                {
                    // Debug.Log("GateA1から出た");
                    _path.Out();
                }
            }
            else if (other.gameObject.CompareTag("GateA2in"))
            {
                if (_preDetectTag.Equals("GateA2out"))
                {
                    // Debug.Log("GateA2から入った");
                    _path.InA2();
                    _answer = false;
                }
            }
            else if (other.gameObject.CompareTag("GateA2out"))
            {
                if (_preDetectTag.Equals("GateA2in"))
                {
                    // Debug.Log("GateA2から出た");
                    _path.Out();
                }
            }
            else if (other.gameObject.CompareTag("GateB1in"))
            {
                if (_preDetectTag.Equals("GateB1out"))
                {
                    // Debug.Log("GateB1から入った");
                    _path.InB1();
                    _answer = true;
                }
            }
            else if (other.gameObject.CompareTag("GateB1out"))
            {
                if (_preDetectTag.Equals("GateB1in"))
                {
                    // Debug.Log("GateB1から出た");
                    _path.Out();
                }
            }
            else if (other.gameObject.CompareTag("GateB2in"))
            {
                if (_preDetectTag.Equals("GateB2out"))
                {
                    // Debug.Log("GateB2から入った");
                    _path.InB2();
                    _answer = false;
                }
            }
            else if (other.gameObject.CompareTag("GateB2out"))
            {
                if (_preDetectTag.Equals("GateB2in"))
                {
                    // Debug.Log("GateB2から出た");
                    _path.Out();
                }
            }
            _preDetectTag = other.tag;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Curpet"))
        {
            _isCurpet = true;
            // Debug.Log("Curpetに乗った");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Curpet"))
        {
            _isCurpet = false;
            // Debug.Log("Curpetから出た");
        }
    }
}
