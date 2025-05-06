using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WebGL_3DSound : MonoBehaviour
{
#if UNITY_WEBGL
    
    private AudioSource _audioSource;
    private VideoPlayer _videoPlayer;
    private GameObject _player;
    private float _maxDistance = 10f;
    private float _maxVolume = 1f;
    void Start()
    {

        _audioSource = gameObject.GetComponent<AudioSource>();
        _videoPlayer = gameObject.GetComponent<VideoPlayer>();
        _maxVolume = _audioSource.volume;
        _videoPlayer.SetDirectAudioVolume(0, _maxVolume); // 音量を設定
        _player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if(distance < _maxDistance)
        {
            float volume = Mathf.Clamp01(_maxVolume - Mathf.Pow(distance / _maxDistance, 2));
            _audioSource.volume = volume;
            _videoPlayer.SetDirectAudioVolume(0, volume); // 音量を設定
        }
        else
        {
            _audioSource.volume = 0f;
            _videoPlayer.SetDirectAudioVolume(0, 0f); // 音量を設定
        }

    }
#endif
}
