using Game.Common.StateMachines;
using Godot;

namespace Game.Entities.Player;

public abstract class PlayerState(Player player, PlayerStateMachine stateMachine)
    : IState<PlayerState>
{
    public Player Player { get; init; } = player;
    public PlayerStateMachine StateMachine { get; protected set; } = stateMachine;

    public virtual void Input(InputEvent @event) { }

    public virtual void UnhandledInput(InputEvent @event) { }

    public virtual void PhysicsUpdate(double delta) { }

    public virtual void Update(double delta) { }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public bool CanTransition(PlayerState current, PlayerState next)
    {
        return true;
    }
}

public class PlayerIdleState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine) { }

public class PlayerMovingState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine) { }

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public override bool CanTransition(PlayerState next)
    {
        return CurrentState.CanTransition(CurrentState, next);
    }
}
