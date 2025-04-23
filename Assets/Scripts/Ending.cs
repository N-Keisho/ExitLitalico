using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class Ending : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private float _inStart = 0.0f;
    [SerializeField] private float _outStart = 1.0f;

    [Header("Objects")]
    [SerializeField] private GameObject _fadeInObj;
    [SerializeField] private GameObject _fadeOutObj;
    [SerializeField] private VideoPlayer _videoPlayer;

    enum FadeType
    {
        In,
        Out
    }

    void Start()
    {
        _fadeInObj.SetActive(true);
        _fadeOutObj.SetActive(false);
        SetAlpha(_fadeInObj, 0f, FadeType.In);
        
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
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

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(_inStart);

        float elapsedTime = 0f;
        _videoPlayer.SetDirectAudioVolume(0, 0f);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeInObj, alpha, FadeType.In);
            _videoPlayer.SetDirectAudioVolume(0, alpha);
            yield return null;
        }
    }


    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_outStart);
        _fadeOutObj.SetActive(true);
        float elapsedTime = 0f;
        _videoPlayer.SetDirectAudioVolume(0, 1f);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeOutObj, alpha, FadeType.Out);
            _videoPlayer.SetDirectAudioVolume(0, 1f - alpha);
            yield return null;
        }
        SceneManager.LoadScene("StaffRoll");
    }
}
