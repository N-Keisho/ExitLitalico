using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Param : MonoBehaviour
{
    [SerializeField] private float _initialValue = 0.5f;
    [SerializeField] private float _minValue = 0f;
    [SerializeField] private float _maxValue = 1f;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _inputField;
    private float _preValue = 0f;
    public float value => _slider.value;

    private void Start()
    {
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        
        if(_slider.value <= _minValue || _slider.value >= _maxValue)
        {
            _slider.value = _initialValue;
        }
        
        _inputField.text = _slider.value.ToString("F2");
    }

    public void OnSliderChange()
    {
        if (_slider.value != _preValue && _slider.value >= _minValue && _slider.value <= _maxValue)
        {
            _preValue = _slider.value;
            _inputField.text = _slider.value.ToString("F2");
        }
        else if (_slider.value < _minValue)
        {
            _slider.value = _minValue;
            _preValue = _slider.value;
            _inputField.text = _slider.value.ToString("F2");
        }
        else if (_slider.value > _maxValue)
        {
            _slider.value = _maxValue;
            _preValue = _slider.value;
            _inputField.text = _slider.value.ToString("F2");
        }
        else
        {
            _slider.value = _preValue;
            _inputField.text = _preValue.ToString("F2");
        }
    }

    public void OnInputFieldChange()
    {
        if (float.TryParse(_inputField.text, out float value) && value >= _minValue && value <= _maxValue)
        {
            _slider.value = value;
            _preValue = _slider.value;
        }
        else if (value < _minValue)
        {
            _slider.value = _minValue;
            _preValue = _slider.value;
            _inputField.text = _slider.value.ToString("F2");
        }
        else if (value > _maxValue)
        {
            _slider.value = _maxValue;
            _preValue = _slider.value;
            _inputField.text = _slider.value.ToString("F2");
        }
        else
        {
            _inputField.text = _preValue.ToString("F2");
        }
    }

    public void SetValue(float value)
    {
        if (value >= _minValue && value <= _maxValue)
        {
            _slider.value = value;
            _inputField.text = value.ToString("F2");
            _preValue = value;
        }
        else if (value < _minValue)
        {
            _slider.value = _minValue;
            _inputField.text = _minValue.ToString("F2");
            _preValue = _minValue;
        }
        else if (value > _maxValue)
        {
            _slider.value = _maxValue;
            _inputField.text = _maxValue.ToString("F2");
            _preValue = _maxValue;
        }
    }


}
