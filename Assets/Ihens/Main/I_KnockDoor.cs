using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_KnockDoor : IhenBase
{
    [Header("Knock Door")]
    [SerializeField] private GameObject _doorObj;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _knockSound = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlaySound", 0f, 2.5f);
    }

    private void PlaySound()
    {
        float volume = 1f;
        StartCoroutine(TrembleDoor());
        int soundIndex = Random.Range(0, _knockSound.Count);
        if (soundIndex == 2)
        {
            volume = 0.25f;
        }
        _audioSource.PlayOneShot(_knockSound[soundIndex], volume);
    }

    private IEnumerator TrembleDoor()
    {
        Vector3 originalPosition = _doorObj.transform.position;

        for(int i = 0; i < 10; i++)
        {
            float diff = Random.Range(-0.025f, 0.025f);
            _doorObj.transform.position = originalPosition + new Vector3(0, 0, diff);
            yield return new WaitForSeconds(0.05f);
            _doorObj.transform.position = originalPosition - new Vector3(0, 0, diff);
            yield return new WaitForSeconds(0.05f);
        }

        _doorObj.transform.position = originalPosition;
    }
}
