using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Skip : MonoBehaviour
{
    [SerializeField] private Image _guage;
    [SerializeField] private string _sceneName = "Main";

    private GameInputs _gameInput;

    void Start()
    {
        _gameInput = new GameInputs();
        _gameInput.System.Skip.Enable();

        _guage.fillAmount = 0f;
        SetActiveChild(false);
    }

    void OnDestroy() {
        _gameInput.System.Skip.Disable();
    }

    void Update()
    {
        float value = _gameInput.System.Skip.GetTimeoutCompletionPercentage();
        if (value > 0f)
        {
            SetActiveChild(true);
            _guage.fillAmount = value;
        }
        else
        {
            SetActiveChild(false);
            _guage.fillAmount = 0f;
        }

        if (value >= 1f)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    void SetActiveChild(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
