using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class I_DammyExit : IhenBase
{
    [Header("Dammy Exit")]
    [SerializeField] private GameObject _exitObj;
    private InputManager _inputManager;
    private GameObject _target;
    void Start()
    {
        _target = GameObject.Find("Player");
        _inputManager = GameObject.Find("_InputManager_").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_target.transform.position, _exitObj.transform.position);
        if(distance < 1.8f)
        {
            _inputManager.Dispose();
            SceneManager.LoadScene("Main");
        }
    }
}
