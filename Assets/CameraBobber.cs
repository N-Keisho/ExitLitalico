using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobber : MonoBehaviour
{

    [SerializeField] private Player _player;
    private HeadBobber bob;
    private Vector3 originalLocalPosition;

    [Header("Footstep Settings")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float footstepInterval = 0.5f;

    private float footstepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bob = GetComponent<HeadBobber>();
        originalLocalPosition = transform.localPosition;
        bob.Initialize();
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
            Vector3 offset = bob.GetVectorOffset(speed);
            transform.localPosition = originalLocalPosition + offset;

            HandleFootstep(speed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, Time.deltaTime * 5f);
            footstepTimer = 0.5f; // 停止時はタイマーリセット
        }
    }

    void HandleFootstep(float speed)
    {
        footstepTimer += Time.deltaTime;

        float interval = footstepInterval / speed; // スピードが速いほど間隔は短く

        if (footstepTimer >= interval)
        {
            PlayFootstepSound();
            footstepTimer = 0f;
        }
    }

    void PlayFootstepSound()
    {
        if (footstepClips.Length > 0 && audioSource != null)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
