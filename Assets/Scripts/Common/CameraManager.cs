using System;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Parameter _parameter;

    [SerializeField]
    private float _baseSize = 9.6f;
    public float BaseSize => _baseSize;

    [SerializeField]
    private float _trackSpped = 6f;

    [SerializeField]
    private float _maxX = 10f;

    private Vector2 _shakeOffset;

    private Sequence _shakeSeq;

    public Parameter Param => _parameter;

    private void Awake()
    {
        Instance = this;

        _shakeSeq = DOTween.Sequence()
            .Append(DOTween.Shake(() => _shakeOffset, offset => _shakeOffset = offset, 0.2f, 0.3f, 100))
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);
    }

    private void LateUpdate()
    {
        if (_parent == null || _camera == null)
        {
            return;
        }

        if (_parameter.trackTarget != null)
        {
            // 被写体がTransformで指定されている場合、positionパラメータに座標を上書き
            UpdateTrackTargetBlend(_parameter, _trackSpped);
        }

        // パラメータを各種オブジェクトに反映
        _parent.position = new Vector3(
            ClampAbs(_parameter.position.x, _maxX),
            _parameter.position.y
        );
        _camera.transform.localPosition = _parameter.offsetPosition + _shakeOffset;
        _camera.orthographicSize = _baseSize * _parameter.sizeWeight;
    }

    public void Shake()
    {
        _shakeOffset = Vector2.zero;
        _shakeSeq.Restart();
    }

    public static void UpdateTrackTargetBlend(Parameter _parameter, float speed)
    {
        _parameter.position = Vector3.Lerp(
                        a: _parameter.position,
                        b: _parameter.trackTarget.position,
                        t: Time.deltaTime * speed
                    );
    }

    private static float ClampAbs(float value, float max)
    {
        var absValue = Mathf.Abs(value);
        var clampValue = Mathf.Min(absValue, max);
        return clampValue * Mathf.Sign(value);
    }

    public void SetBaseSize(float baseSize)
    {
        _baseSize = baseSize;
    }

    /// <summary> カメラのパラメータ </summary>
    [Serializable]
    public class Parameter
    {
        public Transform trackTarget;
        public Vector2 position;
        public float sizeWeight = 1f;
        public Vector2 offsetPosition = Vector2.zero;
    }
}
