using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProLogue : MonoBehaviour
{
    [SerializeField] private List<string> _prologueText = new List<string>();
    [SerializeField] private TMP_Text _prologueTextBox;
    [SerializeField] private Image _fadeInObj;
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private string _nextSceneName = "Main";

    private bool _isTyping = false;
    private bool _isFading = false;
    private int _currentLine = 0;
    private GameInputs _gameInputs;
    void Start()
    {
        _prologueTextBox.text = "";
        
        _gameInputs = new GameInputs();
        _gameInputs.ProLogue.Next.started += OnNext;
        _gameInputs.ProLogue.Next.Enable();

        SetAlpha(0f);

        StartCoroutine(TypeWriter(_prologueText[0]));
    }

    private void OnDestroy()
    {
        _gameInputs.System.Skip.started -= OnNext;
        _gameInputs.ProLogue.Next.Disable();
    }

    private void OnNext(InputAction.CallbackContext context)
    {
        if (context.started && _currentLine < _prologueText.Count - 1)
        {
            _currentLine++;
            
            if(_prologueText[_currentLine] == "")
            {
                _prologueTextBox.text += "\n";
                _currentLine++;
            }
            if(_isTyping)
            {
                StopAllCoroutines();
                _prologueTextBox.text = "";
                for (int i = 0; i < _currentLine; i++)
                {
                    _prologueTextBox.text += _prologueText[i] + "\n";
                }
            }
            StartCoroutine(TypeWriter(_prologueText[_currentLine]));
        }
        else if (context.started && _currentLine >= _prologueText.Count - 1 && !_isFading)
        {
            _isFading = true;
            _gameInputs.ProLogue.Next.Disable();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator TypeWriter(string text)
    {
        _isTyping = true;
        if (text == "")
        {
            _prologueTextBox.text += "\n";
        }
        else
        {
            for (int i = 0; i < text.Length; i++)
            {
                _prologueTextBox.text += text[i];
                yield return new WaitForSeconds(0.05f);
            }
            _prologueTextBox.text += "\n";
        }
        _isTyping = false;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SceneManager.LoadScene(_nextSceneName);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _fadeInObj.color;
        color.a = alpha;
        _fadeInObj.color = color;
    }
}
