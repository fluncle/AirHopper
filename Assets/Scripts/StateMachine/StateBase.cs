public abstract class StateBase
{
    public abstract void Enter();

    public virtual void Update() { }

    public virtual void Exit() { }
}
