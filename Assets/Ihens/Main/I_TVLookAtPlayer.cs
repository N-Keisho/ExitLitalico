using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_TVLookAtPlayer :IhenBase
{
    [Header("TV Look At Player")]
    [SerializeField] private GameObject _TV;
    [SerializeField] private float _rotationBaffY = 0f;
    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        if(_player)
        {
            Vector3 lookPos = _player.transform.position - _TV.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos, Vector3.up) * Quaternion.Euler(0, _rotationBaffY, 0);
            _TV.transform.rotation = rotation;
        }
    }
}
