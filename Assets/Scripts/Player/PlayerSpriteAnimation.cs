using UnityEngine;

[RequireComponent(typeof(SpriteImageAnimationView))]
public class PlayerSpriteAnimation : MonoBehaviour
{
    private SpriteImageAnimationView _spriteAnimationView;

    [SerializeField]
    private Sprite[] _idleSprites;

    [SerializeField]
    private float _idleInterval = 0.1f;

    [SerializeField]
    private Sprite _jumpUpSprite;

    [SerializeField]
    private Sprite _jumpDownSprite;

    [SerializeField]
    private Sprite[] _hurtSprites;

    [SerializeField]
    private float _hurtInterval = 0.1f;

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

    public void JumpUp()
    {
        _spriteAnimationView.Pause();
        _spriteAnimationView.Image.sprite = _jumpUpSprite;
    }

    public void JumpDown()
    {
        _spriteAnimationView.Image.sprite = _jumpDownSprite;
    }

    public void Hurt(int loops = -1)
    {
        _spriteAnimationView.Play(_hurtSprites, _hurtInterval, loops);
    }
}
