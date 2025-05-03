using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_FishSwiming : IhenBase
{
    [Header("Fish Swiming")]
    [SerializeField] private Transform _fishParent;
    [SerializeField] private Transform _fishChild;
    [SerializeField] private List<Transform> _rings = new List<Transform>();
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _swimSpeed = 1.0f;

    private int _ringIndex = 0;
    void Start()
    {
        for (int i = 1; i < _rings.Count; i++)
        {
            _rings[i].gameObject.SetActive(false);
        }

        StartCoroutine(MoveFaster());
    }

    // Update is called once per frame
    void Update()
    {
        _fishParent.position = Vector3.MoveTowards(_fishParent.position, _rings[_ringIndex].position, _moveSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(_rings[_ringIndex].position - _fishParent.position);
        _fishParent.rotation = Quaternion.Slerp(_fishParent.rotation, targetRotation, _rotationSpeed * Time.deltaTime);


        if (Vector3.Distance(_fishParent.position, _rings[_ringIndex].position) < 0.1f)
        {
            _rings[_ringIndex].gameObject.SetActive(false);
            _ringIndex++;
            if (_ringIndex >= _rings.Count)
            {
                _ringIndex = 0;
            }
            _rings[_ringIndex].gameObject.SetActive(true);
        }

        _fishChild.localPosition = new Vector3(0, Mathf.Sin(Time.time * _swimSpeed) * 0.1f, 0);
        _fishChild.localRotation = Quaternion.Euler(0, -90 + Mathf.Sin(Time.time * _swimSpeed * 2f) * 10f, -90);
    }

    IEnumerator MoveFaster()
    {
        yield return new WaitForSeconds(5.0f);
        float buff = 1.0f;
        float maxBuff = 20.0f;
        float time = 0.0f;

        float defaultMoveSpeed = _moveSpeed;
        float defaultRotationSpeed = _rotationSpeed;
        float defaultSwimSpeed = _swimSpeed;
        while (time < 30.0f)
        {
            time += Time.deltaTime / 30.0f;
            buff = Mathf.Lerp(1.0f, maxBuff, time);
            _moveSpeed = defaultMoveSpeed * buff;
            _rotationSpeed = defaultRotationSpeed * buff;
            _swimSpeed = defaultSwimSpeed * buff / 2;
            yield return null;
        }
        _moveSpeed = defaultMoveSpeed * maxBuff;
        _rotationSpeed = defaultRotationSpeed * maxBuff;
        _swimSpeed = defaultSwimSpeed * maxBuff / 2;
    }
}
