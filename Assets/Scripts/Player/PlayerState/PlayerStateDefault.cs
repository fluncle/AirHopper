using UnityEngine;

public class PlayerStateDefault : PlayerStateBase
{
    private Vector2 _velocity;

    public PlayerStateDefault(Player player) : base(player) { }

    public override void Enter()
    {
        _player.InitializeJumpInput();
        _velocity = _player.Rigidbody.velocity;
    }

    public override void Update()
    {
        if (_velocity.y > 0 && _player.Rigidbody.velocity.y <= 0)
        {
            _player.Animation.JumpDown();
        }
        _velocity = _player.Rigidbody.velocity;
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).point.y < _player.Rect.position.y)
            {
                _player.OnGround();
                return;
            }
        }
    }
}
