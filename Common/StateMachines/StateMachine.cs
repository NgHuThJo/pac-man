namespace Game.Common.StateMachines;

public abstract class StateMachine<T>
    where T : IState<T>
{
    public T CurrentState { get; protected set; }

    public void PhysicsUpdate(double delta)
    {
        CurrentState.PhysicsUpdate(delta);
    }

    public void Update(double delta)
    {
        CurrentState.Update(delta);
    }

    public void ChangeState(T nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public virtual bool CanTransition(T next)
    {
        return true;
    }
}
