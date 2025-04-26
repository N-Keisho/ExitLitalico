using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_FallingCurtain : IhenBase
{
   [SerializeField] private Rigidbody _curtainRb;
   private GameObject _player;
   private bool _isFalling = false;
    void Start()
    {
        _player = GameObject.Find("Player");
        _curtainRb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isFalling) return;
        float distance = Vector3.Distance(_player.transform.position, _curtainRb.gameObject.transform.position);
        if (distance < 9f)
        {
            _curtainRb.useGravity = true;
            _isFalling = true;
            Destroy(_curtainRb.gameObject, 3f);
        }
    }
}
