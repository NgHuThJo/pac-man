using Game.Common.StateMachines;
using Game.Utilities.Autoloads;
using Game.World.Maps;
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
    : PlayerState(player, stateMachine)
{
    public override void PhysicsUpdate(double delta)
    {
        if (InputManager.Instance.Current != InputState.Player)
        {
            return;
        }

        var direction = Player.Controller.MovementDirection;

        if (direction != Vector2.Zero)
        {
            Player.CurrentMovementDirection = direction;
            StateMachine.ChangeState(new PlayerMovingState(Player, StateMachine));
        }
    }
}

public class PlayerMovingState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine)
{
    public override void PhysicsUpdate(double delta)
    {
        if (InputManager.Instance.Current != InputState.Player)
        {
            return;
        }

        if (Player.CanMoveInDirection(Player.NextMovementDirection))
        {
            Player.CurrentMovementDirection = Player.NextMovementDirection;
        }

        if (Player.Controller.MovementDirection != Vector2.Zero)
        {
            Player.NextMovementDirection = Player.Controller.MovementDirection;
            Player.TurnArrow(Player.NextMovementDirection);
        }

        Player.Movement.ApplyVelocity(Player.CurrentMovementDirection);

        Player.MoveAndSlide();

        if (Player.Velocity == Vector2.Zero)
        {
            Player.CurrentMovementDirection = Vector2.Zero;
            StateMachine.ChangeState(new PlayerIdleState(Player, StateMachine));
            return;
        }
    }
}

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public override bool CanTransition(PlayerState next)
    {
        return CurrentState.CanTransition(CurrentState, next);
    }
}
