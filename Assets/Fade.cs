using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private float _inStart = 0.0f;
    [SerializeField] private float _outStart = 1.0f;

    [Header("Objects")]
    [SerializeField] private GameObject _fadeInObj;
    [SerializeField] private GameObject _fadeOutObj;

    enum FadeType
    {
        In,
        Out
    }

    void Start()
    {
        _fadeOutObj.SetActive(false);
        _fadeInObj.SetActive(false);
        Invoke("StartFadeIn", _inStart);
        Invoke("StartFadeOut", _outStart);
    }

    private void SetAlpha(GameObject obj, float alpha, FadeType type)
    {
        if (type == FadeType.In)
        {
            Color color = obj.GetComponent<RawImage>().color;
            color.a = alpha;
            obj.GetComponent<RawImage>().color = color;
        }
        else
        {
            Color color = obj.GetComponent<Image>().color;
            color.a = alpha;
            obj.GetComponent<Image>().color = color;
        }
    }


    private void StartFadeIn()
    {
        _fadeInObj.SetActive(true);
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        SetAlpha(_fadeInObj, 0f, FadeType.In);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeInObj, alpha, FadeType.In);
            yield return null;
        }
    }

    private void StartFadeOut()
    {
        _fadeOutObj.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeOutObj, alpha, FadeType.Out);
            yield return null;
        }
    }
}
