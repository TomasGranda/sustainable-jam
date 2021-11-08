public interface StateMachineState
{
    void OnEnterState();
    void ExecuteState();
    void OnExitState();

    void AddTransition(BaseStateMachineState state);

    BaseStateMachineState GetTransition<T>() where T : BaseStateMachineState;
}
