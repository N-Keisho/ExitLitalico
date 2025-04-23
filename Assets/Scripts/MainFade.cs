using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFade : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Image _fadeInObj;

    [Header("Parameters")]
    [SerializeField] private float _fadeDuration = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        _fadeInObj.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        SetAlpha(_fadeInObj, 0f);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeInObj, alpha);
            yield return null;
        }
        _fadeInObj.gameObject.SetActive(false);
    }

    private void SetAlpha(Image obj, float alpha)
    {
        Color color = obj.color;
        color.a = alpha;
        obj.color = color;
    }
}
