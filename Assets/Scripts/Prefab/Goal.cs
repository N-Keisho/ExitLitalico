using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private InputManager _inputManager;
    private void Start()
    {
        _inputManager = GameObject.Find("_InputManager_").GetComponent<InputManager>();
        if (_inputManager == null)
        {
            Logger.Error("InputManager not found in the scene.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inputManager?.Dispose();
            SceneManager.LoadScene("Ending");
        }
    }
}
