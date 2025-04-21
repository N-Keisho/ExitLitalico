using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentNum : MonoBehaviour
{
    [SerializeField] List<string> _words = new List<string>();

    [Header("Text")]
    [SerializeField] private TMP_Text _currentNumText;
    [SerializeField] private TMP_Text _WelcomeText;
    [SerializeField] private TMP_Text _EnterText;

    public void SetCurrentNum(int num)
    {
        if (num < 0 || num > _words.Count)
        {
            Debug.LogError("Invalid number index: " + num);
            return;
        }

        _currentNumText.text = ""; // Clear the text before setting a new one
        for (int i = 0; i < num; i++)
        {
            _currentNumText.text += _words[i];
        }

        if(num == _words.Count)
        {
            _WelcomeText.text = "ありがとう！";
            _EnterText.text = "出口はこちら→";
        }
    }

}
