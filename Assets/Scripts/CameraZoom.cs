using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField] private float _max = 5f;
    [SerializeField] private float _defo = 60f;
    private GameInputs _gameInputs;
    private bool _isZooming = false;

    void Start()
    {
        _camera.fieldOfView = _defo;

        _gameInputs = new GameInputs();
        _gameInputs.Player.Zoom.started += OnZoom;
        _gameInputs.Player.Zoom.canceled += OnZoom;
        _gameInputs.Enable();
    }

    void OnDestroy()
    {
        _gameInputs.Disable();
    }

    void OnZoom(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Zoom in
            _isZooming = true;
            StartCoroutine(ZoomCoroutine(_max));
        }
        else if (context.canceled)
        {
            // Zoom out
            _isZooming = false;
            StartCoroutine(ZoomCoroutine(_defo));
        }
    }

    IEnumerator ZoomCoroutine(float targetFOV)
    {
        float startFOV = _camera.fieldOfView;
        float elapsedTime = 0f;
        bool isZooming = _isZooming;
        while (elapsedTime < _zoomSpeed)
        {
            elapsedTime += Time.deltaTime;
            _camera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / _zoomSpeed);
            yield return null;

            if (isZooming != _isZooming)
            {
                break;
            }
        }

        if (isZooming == _isZooming)
        {
            _camera.fieldOfView = targetFOV;
        }
    }
}
