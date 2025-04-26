using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_BigFish : IhenBase
{
   [Header("Big Fish")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedY = 5f;
    [SerializeField] private float _speedR = 5f;
    [SerializeField] private float _range = 5f;
    private Vector3 _originalPosition;

    void Start()
    {
        _originalPosition = _target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = _originalPosition + new Vector3(0, Mathf.Sin(Time.time * _speedY) * _range, 0);
        _target.position = targetPosition;
        _target.rotation = Quaternion.Euler(0, Mathf.Sin(Time.time * _speedR) * 360, 0);
    }
}
