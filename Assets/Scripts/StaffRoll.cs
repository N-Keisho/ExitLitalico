using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StaffRoll : MonoBehaviour
{
    [Header("TMP_Text")]
    [SerializeField] private TMP_Text _ihenText;
    [SerializeField] private TMP_Text _unknownText;
    [SerializeField] private TMP_Text _allIhenText;

    [Header("RectTransform")]
    [SerializeField] private RectTransform _staffRollRoot;
    [SerializeField] private RectTransform _afterListObj;
    [SerializeField] private RectTransform _endObj;

    [Header("OtherGameObjects")]
    [SerializeField] private Image _fadeImage;
    [SerializeField] private AudioSource _bgmSource;

    [Header("Parameters")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _highSpeed = 10f;
    [SerializeField] private float _afterListOffset = 50f;
    [SerializeField] private float _endPosY = 580f;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private string _nextSceneName = "Main";

    [Header("IhenList")]
    [SerializeField] private IhenList _ihenList;

    private readonly Vector2 _POSITION_ROOT = new Vector2(0, -700f);
    private readonly Vector2 _POSITION_UNKNOWN = new Vector2(0, -3100);
    private float _baseSpeed = 100f;
    private int _unknownCount = 0;
    private bool _isFin = false;
    private bool _isHighSpeed = false;
    private GameInputs _gameInputs;

    void Start()
    {
        _staffRollRoot.anchoredPosition = _POSITION_ROOT;
        _ihenText.text = "";
        _fadeImage.gameObject.SetActive(false);

        for(int i = 0; i < _ihenList.getListLen(); i++)
        {
            IhenBase ihen = _ihenList.getIhen(i);
            if (GV.IsDoneIhen(ihen.name))
            {
                _ihenText.text += ihen.Explanation + "\n";
            }
            else
            {
                _ihenText.text += "???" + "\n";
                _unknownCount++;
            }
        }

        _afterListObj.anchoredPosition = _POSITION_UNKNOWN - new Vector2(0, _ihenList.getListLen() * _afterListOffset);
        _unknownText.text = _unknownCount.ToString();
        _allIhenText.text = _ihenList.getListLen().ToString();

        _gameInputs = new GameInputs();
        _gameInputs.Player.Dash.started += ctx => _isHighSpeed = true;
        _gameInputs.Player.Dash.canceled += ctx => _isHighSpeed = false;
        _gameInputs.Player.Dash.Enable();
    }

    void Update()
    {
        if (_endObj.position.y <= _endPosY)
        {
            if (!_isHighSpeed)
            {
                _staffRollRoot.anchoredPosition += new Vector2(0, _speed * Time.deltaTime * _baseSpeed);
            }
            else
            {
                _staffRollRoot.anchoredPosition += new Vector2(0, _highSpeed * Time.deltaTime * _baseSpeed);
            }


        }
        else if (!_isFin)
        {
            _isFin = true;
            StartCoroutine(FadeOut());
        }
    }

    private void OnDestroy()
    {
        GV.isClear = true;
        _gameInputs.Disable();
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_waitTime);

        float elapsedTime = 0f;
        Color color = _fadeImage.color;
        color.a = 0f;
        _fadeImage.color = color;
        _fadeImage.gameObject.SetActive(true);
        float _bgmVolume = _bgmSource.volume;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / _fadeDuration);
            _fadeImage.color = color;
            _bgmSource.volume = Mathf.Lerp(_bgmVolume, 0f, elapsedTime / _fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(_nextSceneName);
    }
}
