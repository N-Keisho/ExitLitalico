using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private GameObject _stopperA1;
    [SerializeField] private GameObject _stopperA2;
    [SerializeField] private GameObject _stopperB1;
    [SerializeField] private GameObject _stopperB2;
    void Start()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(true);
        _stopperB2.SetActive(true);
    }

    public void InA1()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);
    }

    public void InA2()
    {
        _stopperA1.SetActive(true);
        _stopperA2.SetActive(false);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);
    }

    public void InB1()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);
    }
    public void InB2()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(true);
        _stopperB2.SetActive(false);
    }

    public void Out(){
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(false);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(false);
    }
}
