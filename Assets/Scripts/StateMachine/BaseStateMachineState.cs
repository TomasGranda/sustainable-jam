using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachineState : StateMachineState
{
    List<BaseStateMachineState> statesList = new List<BaseStateMachineState>();

    public abstract void OnEnterState();
    public abstract void ExecuteState();
    public abstract void OnExitState();

    public void AddTransition(BaseStateMachineState state)
    {
        if (!statesList.Exists((s) => s.GetType() == state.GetType()))
            statesList.Add(state);
    }

    public BaseStateMachineState GetTransition<T>() where T : BaseStateMachineState
    {
        if (statesList.Exists((s) => s is T)) return statesList.Find((s) => s is T);

        return null;
    }
}
