using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private TMP_Text _pressStartText;
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _se;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _blinkSpeed = 1f;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private string _nextSceneName = "Main";

    private GameInputs _gameInputs;
    private bool _isFading = false;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SetAlpha(0f);

        _gameInputs = new GameInputs();
        _gameInputs.Title.Start.started += OnStart;
        _gameInputs.Title.Start.Enable();

        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        _gameInputs.Title.Start.started -= OnStart;
        _gameInputs.Title.Start.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _mainCamera.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        _pressStartText.color = new Color(_pressStartText.color.r, _pressStartText.color.g, _pressStartText.color.b, Mathf.PingPong(Time.time * _blinkSpeed, 1f));
    }
    
    private void OnStart(InputAction.CallbackContext context)
    {
        if (context.started && !_isFading)
        {
            _isFading = true;
            _gameInputs.Title.Start.Disable();
            _bgmSource.Stop();
            _bgmSource.volume = 0.5f;
            _bgmSource.PlayOneShot(_se);
            StartCoroutine(FadeOut());
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = _fadeImage.color;
        color.a = alpha;
        _fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        _blinkSpeed *= 10f;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(_nextSceneName);
    }
}
