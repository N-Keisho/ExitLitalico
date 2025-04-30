using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_WaterServer : IhenBase
{
    [Header("Water Server")]
    [SerializeField] private GameObject _water;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private float _maxSize = 10f;
    void Start()
    {
        if (_water == null)
        {
            Logger.Error("Water GameObject is not assigned.");
            return;
        }

        StartCoroutine(StartWater());
    }

    IEnumerator StartWater()
    {
        float time = 0f;
        Vector3 startScale = _water.transform.localScale;
        Vector3 endScale = new Vector3(_maxSize, _maxSize, _maxSize);
        while (time < _duration)
        {
            time += Time.deltaTime;
            float t = time / _duration;
            _water.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }
}
