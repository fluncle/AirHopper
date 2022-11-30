using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private int _dragMaxDistance = 180;
    /// <summary>最大入力と判定するドラッグ量（ピクセル）</summary>
    public int DragMaxDistance => _dragMaxDistance;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private RectTransform _baseRect;

    private Vector2 _basePosition;

    private Vector2 _dragPosition;

    public Action<Vector2> OnPointerDownEvent;

    /// <summary>
    /// ドラッグ開始時のイベント
    /// 引数1: ドラッグ座標
    /// 引数2: ドラッグ開始座標
    /// </summary>
    public Action<Vector2, Vector2> OnBeginDragEvent;

    /// <summary>
    /// ドラッグ中のイベント
    /// 引数1: ドラッグ座標
    /// 引数2: 基点座標
    /// </summary>
    public Action<Vector2, Vector2> OnDragEvent;

    /// <summary>
    /// タッチ終了時のイベント
    /// 引数1: ドラッグ座標
    /// 引数2: 基点座標
    /// </summary>
    public Action<Vector2, Vector2> OnPointerUpEvent;

    /// <summary>入力方向</summary>
    public Vector2 Vector { get; private set; }

    /// <summary>ドラッグ量の割合（0〜1）</summary>
    public float Rate { get; private set; }

    /// <summary>ドラッグ中か否か</summary>
    public bool IsDrag { get; private set; }

    private void Awake()
    {
        if (_baseRect == null)
        {
            _baseRect = GetComponent<RectTransform>();
        }
    }

    /// <summary>各種変数やイベントをクリア</summary>
    public void Clear()
    {
        _basePosition = _dragPosition = Vector3.zero;
        OnPointerDownEvent = null;
        OnBeginDragEvent = null;
        OnDragEvent = null;
        OnPointerUpEvent = null;
        Vector = Vector2.zero;
        Rate = 0f;
        IsDrag = false;
    }

    private Vector2 GetLocalPoint(Vector2 screenPoint)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPoint, _canvas.worldCamera, out Vector2 localPoint);
        return localPoint;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _basePosition = _dragPosition = GetLocalPoint(eventData.position);

        OnPointerDownEvent?.Invoke(_basePosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDrag = true;
        _dragPosition = GetLocalPoint(eventData.position);

        OnBeginDragEvent?.Invoke(_dragPosition, _basePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragPosition = GetLocalPoint(eventData.position);

        var dragVector = _dragPosition - _basePosition;
        var dragDistance = Mathf.Min(dragVector.magnitude, _dragMaxDistance);
        dragVector.Normalize();

        Rate = dragDistance / _dragMaxDistance;
        Vector = dragVector * Rate;

        OnDragEvent?.Invoke(_dragPosition, _basePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(_dragPosition, _basePosition);

        IsDrag = false;
        _basePosition = _dragPosition = Vector3.zero;
        Vector = Vector2.zero;
        Rate = 0f;
    }
}
