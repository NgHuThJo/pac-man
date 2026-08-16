using Godot;

namespace Game.Common.StateMachines;

public interface IState<T>
    where T : IState<T>
{
    public void Input(InputEvent @event);
    public void UnhandledInput(InputEvent @event);
    public void PhysicsUpdate(double delta);
    public void Update(double delta);
    public void Enter();
    public void Exit();
    public bool CanTransition(T current, T next);
}
