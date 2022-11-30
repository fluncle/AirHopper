using UnityEngine;

/// <summary>
/// セーフエリアに対応してRectTransformを自動リサイズする
/// 引用元: https://kingmo.jp/kumonos/unity-safearea-ios-android/
/// </summary>
public class SafeAreaAdjuster : MonoBehaviour
{
    //セーフエリアに合わせたい箇所をtrueにする。
    [SerializeField] bool left;
    [SerializeField] bool right;
    [SerializeField] bool top;
    [SerializeField] bool bottom;

    private void Start()
    {

        var panel = GetComponent<RectTransform>();
        var area = Screen.safeArea;

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;

        if (left) anchorMin.x /= Screen.width;
        else anchorMin.x = 0;

        if (right) anchorMax.x /= Screen.width;
        else anchorMax.x = 1;

        if (bottom) anchorMin.y /= Screen.height;
        else anchorMin.y = 0;

        if (top) anchorMax.y /= Screen.height;
        else anchorMax.y = 1;

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;

    }
}
