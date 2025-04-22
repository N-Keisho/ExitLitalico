using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("Stopper")]
    [SerializeField] private GameObject _stopperA1;
    [SerializeField] private GameObject _stopperA2;
    [SerializeField] private GameObject _stopperB1;
    [SerializeField] private GameObject _stopperB2;

    [Header("Poster")]
    [SerializeField] private SpriteRenderer _posterA;
    [SerializeField] private SpriteRenderer _posterB;

    [Header("Poster Sprite")]
    [SerializeField] private Sprite _watch;
    [SerializeField] private Sprite _follow;
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

        _posterA.sprite = null;
        _posterB.sprite = _follow;
    }

    public void InA2()
    {
        _stopperA1.SetActive(true);
        _stopperA2.SetActive(false);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);

        _posterA.sprite = null;
        _posterB.sprite = _follow;
    }

    public void InB1()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);

        _posterA.sprite = _follow;
        _posterB.sprite = null;
    }
    public void InB2()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(true);
        _stopperB2.SetActive(false);

        _posterA.sprite = _follow;
        _posterB.sprite = null;
    }

    public void CheckA()
    {
        _stopperA1.SetActive(true);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(true);

        _posterA.sprite = _watch;
    }
    public void CheckB()
    {
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(true);
        _stopperB1.SetActive(true);
        _stopperB2.SetActive(true);

        _posterB.sprite = _watch;
    }

    public void Out(){
        _stopperA1.SetActive(false);
        _stopperA2.SetActive(false);
        _stopperB1.SetActive(false);
        _stopperB2.SetActive(false);
    }
}
