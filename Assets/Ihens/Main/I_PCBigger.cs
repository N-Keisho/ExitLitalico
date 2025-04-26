using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PCBigger : IhenBase
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _maxSize = 1.0f;

    [Header("PC List")]
    [SerializeField] private List<GameObject> _PCList = new List<GameObject>();

    private bool _isGrowing = true;

    // Update is called once per frame
    void Update()
    {
        if (_isGrowing)
        {
            GrowPCs();
        }
    }

    private void GrowPCs()
    {
        foreach (GameObject pc in _PCList)
        {
            if (pc.transform.localScale.x < _maxSize)
            {
                float newScaleBaff = _speed * Time.deltaTime;
                pc.transform.localScale += new Vector3(newScaleBaff, newScaleBaff, newScaleBaff);
            }
            else
            {
                _isGrowing = false;
                break;
            }
        }
    }
}
