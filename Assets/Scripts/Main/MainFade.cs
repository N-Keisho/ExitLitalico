using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SetAlpha(_fadeInObj, 1f);
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / _fadeDuration);
            SetAlpha(_fadeInObj, alpha);
            yield return null;
        }
        _fadeInObj.gameObject.SetActive(false);
    }

    public void FadeOut(string sceneName, float fadeOutDuration = 1.0f)
    {
        _fadeInObj.gameObject.SetActive(true);
        StartCoroutine(FadeOutAndLoadScene(sceneName, fadeOutDuration));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName, float fadeOutDuration = 1.0f)
    {
        float elapsedTime = 0f;
        SetAlpha(_fadeInObj, 0f);
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeOutDuration);
            SetAlpha(_fadeInObj, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene(sceneName);
    }

    private void SetAlpha(Image obj, float alpha)
    {
        Color color = obj.color;
        color.a = alpha;
        obj.color = color;
    }
}
