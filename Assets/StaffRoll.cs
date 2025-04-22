using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StaffRoll : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private RectTransform _staffRollRoot;
    [SerializeField] private TMP_Text _ihenText;
    [SerializeField] private RectTransform _afterListObj;
    [SerializeField] private TMP_Text _unknownText;
    [SerializeField] private TMP_Text _allIhenText;
    [SerializeField] private RectTransform _endObj;
    [SerializeField] private Image _fadeImage;

    [Header("Parameters")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _highSpeed = 10f;
    [SerializeField] private float _afterListOffset = 50f;
    [SerializeField] private float _endPosY = 580f;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _fadeDuration = 1f;

    [Header("IhenList")]
    [SerializeField] private List<IhenBase> _ihenList = new List<IhenBase>();

    private readonly Vector2 _POSITION_ROOT = new Vector2(0, -700f);
    private readonly Vector2 _POSITION_UNKNOWN = new Vector2(0, -3000);
    private float _baseSpeed = 100f;
    private int _unknownCount = 0;
    private bool _isFin = false;

    void Start()
    {
        _staffRollRoot.anchoredPosition = _POSITION_ROOT;
        _ihenText.text = "";
        _fadeImage.gameObject.SetActive(false);

        foreach (IhenBase ihen in _ihenList)
        {
            GV.SetDoneIhen(ihen.name, false);
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

        _afterListObj.anchoredPosition = _POSITION_UNKNOWN - new Vector2(0, _ihenList.Count * _afterListOffset);
        _unknownText.text = _unknownCount.ToString();
        _allIhenText.text = _ihenList.Count.ToString();
    }

    void Update()
    {
        if (_endObj.position.y <= _endPosY)
        {
            _staffRollRoot.anchoredPosition += new Vector2(0, _speed * Time.deltaTime * _baseSpeed);
        }
        else if (!_isFin)
        {
            _isFin = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_waitTime);

        float elapsedTime = 0f;
        Color color = _fadeImage.color;
        color.a = 0f;
        _fadeImage.color = color;
        _fadeImage.gameObject.SetActive(true);

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / _fadeDuration);
            _fadeImage.color = color;
            yield return null;
        }
    }
}
