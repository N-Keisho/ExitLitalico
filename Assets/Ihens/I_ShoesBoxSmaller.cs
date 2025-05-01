using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_ShoesBoxSmaller : IhenBase
{
    [Header("Shoes Box Smaller")]
    [SerializeField] private GameObject _shoesBox;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _scaleFactor = 0.1f;

    void Start()
    {
        StartCoroutine(SmallerCoroutine());
    }

    IEnumerator SmallerCoroutine()
    {
        Vector3 originalScale = _shoesBox.transform.localScale;
        Vector3 targetScale = originalScale * _scaleFactor;

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _duration);
            _shoesBox.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
    }
}
