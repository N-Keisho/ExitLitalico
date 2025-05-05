using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    // VideoPlayerコンポーネント
    [SerializeField] private VideoPlayer _videoPlayer;

    // StreamingAssetsの動画ファイルへのパス
    [SerializeField] private string _streamingAssetsMoviePath;
    [SerializeField] private float _videoSpeed = 1f; // 再生速度
    [SerializeField] private float _waitTime = 1f; // 待機時間

    private void Awake()
    {
        // URL指定
        _videoPlayer.source = VideoSource.Url;

        #if UNITY_WEBGL
        _videoPlayer.url = "https://n-keisho.github.io/ExitLitalico/videos/" + _streamingAssetsMoviePath;
        #else
        // _videoPlayer.url = Application.streamingAssetsPath + "/" + _streamingAssetsMoviePath;
        #endif
        
        _videoPlayer.playbackSpeed = _videoSpeed;

        Invoke("Play", _waitTime);
    }

    private void OnDestroy()
    {
        if (_videoPlayer != null && _videoPlayer.targetTexture != null)
        {
            // VideoPlayerのtargetTextureを解放する
            _videoPlayer.targetTexture.Release();
        }
    }

    private void Play()
    {
        _videoPlayer.Play();
    }
}
