using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_ChangeLightColor : IhenBase
{
    [Header("Change Light Color")]
    [ColorUsage(true, true)]
    [SerializeField] private Color _targetColor = Color.red;
    [SerializeField] private MeshRenderer _targetRenderer;
    [SerializeField] private float _waitForTime = 5f;
    [SerializeField] private float _changeDuration = 1f;
    private Color _originalColor;

    void Start()
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        if (_targetRenderer != null)
        {
            yield return new WaitForSeconds(_waitForTime);
            _originalColor = _targetRenderer.sharedMaterial.GetColor("_EmissionColor");
            float elapsedTime = 0f;

            while (elapsedTime < _changeDuration)
            {
                elapsedTime += Time.deltaTime;
                _targetRenderer.sharedMaterial.SetColor("_EmissionColor", Color.Lerp(_originalColor, _targetColor, elapsedTime / _changeDuration));
                yield return null;
            }
        }
    }

    void OnDestroy() {
        if (_targetRenderer != null)
        {
            _targetRenderer.sharedMaterial.SetColor("_EmissionColor", _originalColor);
        }
    }
}
