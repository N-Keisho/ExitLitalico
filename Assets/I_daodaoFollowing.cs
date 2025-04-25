using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class I_daodaoFollowing : IhenBase
{
    [Header("Daodao Following")]
    [SerializeField] private NavMeshAgent _daodao;
    [SerializeField] private GameObject _daodaoFacePoint;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _daodaoSound = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _daodaoChatchSound = new List<AudioClip>();

    [Header("Parameters")]
    [SerializeField] private float _soundInterval = 2f;
    [SerializeField] private float _freezeTime = 2f;
    private GameObject _target;
    private InputManager _inputManager;
    private bool _isGameOver = false;
    private int _preSoundIndex = -1;
    void Start()
    {
        _target = GameObject.Find("Player");
        _inputManager = GameObject.Find("_InputManager_").GetComponent<InputManager>();
        InvokeRepeating("DaodaoSound", _soundInterval, _soundInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) 
        {
            _target.transform.LookAt(_daodao.transform.position);
            return;
        }
        
        _daodao.SetDestination(_target.transform.position);
        _daodao.transform.LookAt(_target.transform.position);

        if (Vector3.Distance(_daodao.transform.position, _target.transform.position) < 1.5f)
        {
            _isGameOver = true;
            _daodao.isStopped = true;
            _inputManager.Dispose();
            CancelInvoke("DaodaoSound");
            _audioSource.PlayOneShot(_daodaoChatchSound[Random.Range(0, _daodaoChatchSound.Count)]);
            Invoke("ToMain", _freezeTime);
        }
    }

    private void DaodaoSound()
    {
        if (_isGameOver) return;
        int soundIndex = Random.Range(0, _daodaoSound.Count);
        while (soundIndex == _preSoundIndex)
        {
            soundIndex = Random.Range(0, _daodaoSound.Count);
        }
        _audioSource.PlayOneShot(_daodaoSound[soundIndex]);
        _preSoundIndex = soundIndex;
    }

    private void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
