using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _rotationSpeed = 10f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _mainCamera.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
