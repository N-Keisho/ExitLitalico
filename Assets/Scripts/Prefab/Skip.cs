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
    private bool _isClear = false;

    void Start()
    {
        _gameInput = new GameInputs();
        _gameInput.System.Skip.Enable();

        _guage.fillAmount = 0f;
        _isClear = GV.isClear;

        if (!_isClear)
        {
            SetActiveChild(false);
        }
    }

    void OnDestroy()
    {
        _gameInput.System.Skip.Disable();
    }

    void Update()
    {
        float value = _gameInput.System.Skip.GetTimeoutCompletionPercentage();
        if (value > 0f)
        {
            _guage.fillAmount = value;
            if (!_isClear)
            {
                SetActiveChild(true);
            }
        }
        else
        {
            _guage.fillAmount = 0f;
            if (!_isClear)
            {
                SetActiveChild(false);
            }
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
