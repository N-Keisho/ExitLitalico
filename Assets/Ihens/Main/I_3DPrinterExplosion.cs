using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_3DPrinterExplosion : IhenBase
{
    [Header("3D Printer Explosion")]
    [SerializeField] private List<GameObject> _3DPrinters = new List<GameObject>();
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _explosionSound = new List<AudioClip>();
    private GameObject _player;
    private Vector3 _audioPosition;
    private bool _explosed = false;
    void Start()
    {
        _player = GameObject.Find("Player");
        _audioPosition = _audioSource.transform.position;
    }

    void Update()
    {
        if(_explosed) return;
        float distance = Vector3.Distance(_audioPosition, _player.transform.position);
        if (distance < 10f && !_explosed)
        {
            _explosed = true;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject printer in _3DPrinters)
        {
            if (printer != null)
            {
                GameObject explosion = Instantiate(_explosionPrefab, printer.transform.position, Quaternion.identity);
                explosion.transform.localScale = printer.transform.localScale;
                _audioSource.PlayOneShot(_explosionSound[Random.Range(0, _explosionSound.Count)]);
                Destroy(printer);
                Destroy(explosion, 2f);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
