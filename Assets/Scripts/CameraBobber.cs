using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobber : MonoBehaviour
{

    [SerializeField] private Player _player;
    private HeadBobber _bob;
    private Vector3 _originalLocalPosition;

    [Header("Footstep Settings")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footstepAsfaults;
    [SerializeField] private AudioClip[] _footstepCurpets;
    [SerializeField] private float _footstepInterval = 0.5f;

    private float footstepTimer;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _bob = GetComponent<HeadBobber>();
        _originalLocalPosition = transform.localPosition;
        _bob.Initialize();
    }

    void Update()
    {
        float speed = 0f;

        if (_player.IsMove && !_player.IsDash)
        {
            speed = 2f;
        }
        else if (_player.IsMove && _player.IsDash)
        {
            speed = 4f;
        }

        if (speed > 0f)
        {
            Vector3 offset = _bob.GetVectorOffset(speed);
            transform.localPosition = _originalLocalPosition + offset;

            HandleFootstep(speed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalLocalPosition, Time.deltaTime * 5f);
            footstepTimer = 0.5f; // 停止時はタイマーリセット
        }
    }

    void HandleFootstep(float speed)
    {
        footstepTimer += Time.deltaTime;

        float interval = _footstepInterval / speed; // スピードが速いほど間隔は短く

        if (footstepTimer >= interval)
        {
            PlayFootstepSound();
            footstepTimer = 0f;
        }
    }

    void PlayFootstepSound()
    {
        if (_footstepCurpets.Length > 0 && _footstepAsfaults.Length > 0 &&_audioSource != null)
        {
            AudioClip clip = _player.IsCurpet ? _footstepCurpets[Random.Range(0, _footstepCurpets.Length)] : _footstepAsfaults[Random.Range(0, _footstepAsfaults.Length)];
            _audioSource.PlayOneShot(clip);
        }
    }
}
