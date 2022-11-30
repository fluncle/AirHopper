using UnityEngine;

public abstract class PlayerStateBase : StateBase
{
    protected Player _player;

    public PlayerStateBase(Player player) : base()
    {
        _player = player;
    }

    public virtual void OnCollisionEnter(Collision2D collision) { }
}
