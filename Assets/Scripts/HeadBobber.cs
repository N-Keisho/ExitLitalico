using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _bobCurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.5f, 1f),
        new Keyframe(1f, 0f),
        new Keyframe(1.5f, -1f),
        new Keyframe(2f, 0f)
    );

    [SerializeField] float _horizontalMultiplier = 0.01f;
    [SerializeField] float _verticalMultiplier = 0.02f;
    [SerializeField] float _verticalToHorizontalSpeedRatio = 2.0f;
    [SerializeField] float _baseInterval = 1.0f;

    private float _xPlayhead;
    private float _yPlayhead;
    private float _curveEndTime;
    private float _bobber_buf = 1f;

    public void Initialize()
    {
        _curveEndTime = _bobCurve[_bobCurve.length - 1].time;
        _xPlayhead = _yPlayhead = 0f;
    }

    public Vector3 GetVectorOffset(float speed)
    {
        float delta = (speed * Time.deltaTime) / _baseInterval;
        _xPlayhead = (_xPlayhead + delta) % _curveEndTime;
        _yPlayhead = (_yPlayhead + delta * _verticalToHorizontalSpeedRatio) % _curveEndTime;

        float xPos = _bobCurve.Evaluate(_xPlayhead) * _horizontalMultiplier * _bobber_buf;
        float yPos = _bobCurve.Evaluate(_yPlayhead) * _verticalMultiplier * _bobber_buf;

        return new Vector3(xPos, yPos, 0f);
    }

    public void SetBobber(float value)
    {
        _bobber_buf = value;
    }
}
