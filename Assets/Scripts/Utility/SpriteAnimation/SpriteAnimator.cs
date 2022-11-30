using UnityEngine;

/// <summary>
/// SpriteAnimationViewで1パターンのアニメーションをシンプルに再生するコンポーネント
/// </summary>
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private string _spritePathFormat;

    [SerializeField]
    private SpriteAnimationViewBase.SpriteMode _spriteMode;

    [SerializeField]
    private int _count;

    [SerializeField]
    private int _startIndex;

    [SerializeField]
    private float _interval = 0.1f;

    [SerializeField]
    private int _loops = -1;

    private SpriteAnimationViewBase _spriteAnimationView;

    private void Start()
    {
        _spriteAnimationView = GetComponent<SpriteAnimationViewBase>();
        PlayAnimation();
    }

    [ContextMenu("Play Animation")]
    private void PlayAnimation()
    {
        _spriteAnimationView.SetSpriteMode(_spriteMode);
        _spriteAnimationView.Play(_spritePathFormat, _count, _startIndex, _interval, _loops);
    }
}
