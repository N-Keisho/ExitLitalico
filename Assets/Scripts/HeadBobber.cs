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

    private float xPlayhead;
    private float yPlayhead;
    private float curveEndTime;
    private float _bobber_buf = 1f;

    public void Initialize()
    {
        curveEndTime = _bobCurve[_bobCurve.length - 1].time;
        xPlayhead = yPlayhead = 0f;
    }

    public Vector3 GetVectorOffset(float speed)
    {
        float delta = (speed * Time.deltaTime) / _baseInterval;
        xPlayhead = (xPlayhead + delta) % curveEndTime;
        yPlayhead = (yPlayhead + delta * _verticalToHorizontalSpeedRatio) % curveEndTime;

        float xPos = _bobCurve.Evaluate(xPlayhead) * _horizontalMultiplier * _bobber_buf;
        float yPos = _bobCurve.Evaluate(yPlayhead) * _verticalMultiplier * _bobber_buf;

        return new Vector3(xPos, yPos, 0f);
    }

    public void SetBobber(float value)
    {
        _bobber_buf = value;
    }
}
