using UnityEngine;

[RequireComponent(typeof(SpriteImageAnimationView))]
public class EnemySpriteAnimation : MonoBehaviour
{
    private SpriteImageAnimationView _spriteAnimationView;

    [SerializeField]
    private Sprite[] _idleSprites;

    [SerializeField]
    private float _idleInterval = 0.1f;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _spriteAnimationView = GetComponent<SpriteImageAnimationView>();
        Idle();
    }

    public void SetFlipX(bool flipX)
    {
        var localScale = _spriteAnimationView.Image.rectTransform.localScale;
        localScale.x = flipX ? -1 : 1;
        _spriteAnimationView.Image.rectTransform.localScale = localScale;
    }

    public void Idle()
    {
        _spriteAnimationView.Play(_idleSprites, _idleInterval);
    }
}
