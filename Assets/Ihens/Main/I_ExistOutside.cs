using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_ExistOutside : IhenBase
{
    [Header("Exitst Outside")]
    [SerializeField] private GameObject _eyesObj;
    private GameObject _player;
    private Vector3 _eyesPos;
    void Start()
    {
        _eyesPos = _eyesObj.transform.position;
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = _player.transform.position;
        _eyesObj.transform.position = new Vector3(_eyesPos.x, _eyesPos.y, playerPos.z);
    }
}
