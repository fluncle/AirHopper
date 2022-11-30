using System.Collections.Generic;
using UnityEngine;

public class StateMachine<Key, State> where Key : struct where State : StateBase
{
    private Dictionary<Key, State> _status;

    public State CurrentState { get; private set; }

    public Key CurrentStateType { get; private set; }

    public StateMachine()
    {
        _status = new Dictionary<Key, State>();
    }

    public void RegisterState(Key stateType, State state)
    {
        if (!typeof(Key).IsEnum)
        {
            Debug.LogError("stateType is not enum.");
            return;
        }

        if (_status.ContainsKey(stateType))
        {
            Debug.LogError("stateType is already registered.");
            return;
        }

        _status.Add(stateType, state);
    }

    public StateBase Transition(Key stateType)
    {
        if (!_status.ContainsKey(stateType))
        {
            Debug.LogError("stateType is not registered.");
            return null;
        }

        CurrentState?.Exit();

        CurrentStateType = stateType;

        CurrentState = _status[stateType];
        CurrentState.Enter();
        return CurrentState;
    }

    public void Process()
    {
        CurrentState?.Update();
    }
}
