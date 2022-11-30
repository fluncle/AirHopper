using UnityEngine;

public static class TransformExtensions
{
    #region Position
    public static void SetPositionX(this Transform transform, float x)
    {
        var position = transform.position;
        position.x = x;
        transform.position = position;
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        var position = transform.position;
        position.y = y;
        transform.position = position;
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        var position = transform.position;
        position.z = z;
        transform.position = position;
    }
    #endregion // Position

    #region LocalPosition
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        var localPosition = transform.localPosition;
        localPosition.x = x;
        transform.localPosition = localPosition;
    }

    public static void SetLocalPositionY(this Transform transform, float y)
    {
        var localPosition = transform.localPosition;
        localPosition.y = y;
        transform.localPosition = localPosition;
    }

    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        var localPosition = transform.localPosition;
        localPosition.z = z;
        transform.localPosition = localPosition;
    }
    #endregion // LocalPosition

    #region Angle
    public static void SetAngleX(this Transform transform, float x)
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.x = x;
        transform.eulerAngles = eulerAngles;
    }

    public static void SetAngleY(this Transform transform, float y)
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.y = y;
        transform.eulerAngles = eulerAngles;
    }

    public static void SetAngleZ(this Transform transform, float z)
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.z = z;
        transform.eulerAngles = eulerAngles;
    }
    #endregion // Angle

    #region LocalScale
    public static void SetLocalScaleX(this Transform transform, float x)
    {
        var localScale = transform.localScale;
        localScale.x = x;
        transform.localScale = localScale;
    }

    public static void SetLocalScaleY(this Transform transform, float y)
    {
        var localScale = transform.localScale;
        localScale.y = y;
        transform.localScale = localScale;
    }

    public static void SetLocalScaleZ(this Transform transform, float z)
    {
        var localScale = transform.localScale;
        localScale.z = z;
        transform.localScale = localScale;
    }
    #endregion // LocalScale

    #region AnchoredPosition
    public static void SetAnchoredPositionX(this RectTransform rect, float x)
    {
        var anchoredPosition = rect.anchoredPosition;
        anchoredPosition.x = x;
        rect.anchoredPosition = anchoredPosition;
    }

    public static void SetAnchoredPositionY(this RectTransform rect, float y)
    {
        var anchoredPosition = rect.anchoredPosition;
        anchoredPosition.y = y;
        rect.anchoredPosition = anchoredPosition;
    }
    #endregion // AnchoredPosition

    #region AnchoredPosition
    public static void SetSizeDeltaX(this RectTransform rect, float x)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta.x = x;
        rect.sizeDelta = sizeDelta;
    }

    public static void SetSizeDeltaY(this RectTransform rect, float y)
    {
        var sizeDelta = rect.sizeDelta;
        sizeDelta.y = y;
        rect.sizeDelta = sizeDelta;
    }
    #endregion // AnchoredPosition
}
