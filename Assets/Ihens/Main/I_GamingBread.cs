using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_GamingBread : IhenBase
{
    [Header("Gaming Bread")]
    [SerializeField] private Material _breadMaterial;
    [SerializeField] private float _speed = 1f;

    private Color _originalColor;


    // Start is called before the first frame update
    void Start()
    {
        if (_breadMaterial == null)
        {
            Logger.Error("Bread material is not assigned.");
            return;
        }

        _originalColor = _breadMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        float hue = Mathf.PingPong(Time.time * _speed, 1f);
        _breadMaterial.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    void OnDestroy()
    {
        _breadMaterial.color = _originalColor;
    }
}
