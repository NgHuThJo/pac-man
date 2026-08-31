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
    : PlayerState(player, stateMachine)
{
    public override void PhysicsUpdate(double delta)
    {
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
        if (Player.Controller.MovementDirection != Vector2.Zero)
        {
            Player.NextMovementDirection = Player.Controller.MovementDirection;
        }

        if (Player.CanMoveInDirection(Player.NextMovementDirection))
        {
            Player.CurrentMovementDirection = Player.NextMovementDirection;
            Player.NextMovementDirection = Vector2.Zero;
            Player.TurnPlayer(Player.CurrentMovementDirection);
        }

        if (!Player.CanMoveInDirection(Player.CurrentMovementDirection))
        {
            Player.CurrentMovementDirection = Vector2.Zero;
            Player.Movement.ApplyVelocity(Vector2.Zero);
            StateMachine.ChangeState(new PlayerIdleState(Player, StateMachine));
            return;
        }

        Player.Movement.ApplyVelocity(Player.CurrentMovementDirection);

        Player.MoveAndSlide();
    }
}

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public override bool CanTransition(PlayerState next)
    {
        return CurrentState.CanTransition(CurrentState, next);
    }
}
