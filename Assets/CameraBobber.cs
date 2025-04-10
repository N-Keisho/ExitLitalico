using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobber : MonoBehaviour
{

    [SerializeField] private Player _player;
    private HeadBobber bob;
    private Vector3 originalLocalPosition;

    void Start()
    {
        bob = GetComponent<HeadBobber>();

        originalLocalPosition = transform.localPosition;
        bob.Initialize();
    }

    void Update()
    {

        if (_player.IsMove && !_player.IsDash)
        {
            Vector3 offset = bob.GetVectorOffset(2f);
            transform.localPosition = originalLocalPosition + offset;
        }
        else if (_player.IsDash)
        {
            Vector3 offset = bob.GetVectorOffset(4f);
            transform.localPosition = originalLocalPosition + offset;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, Time.deltaTime * 5f);
        }
    }
}
