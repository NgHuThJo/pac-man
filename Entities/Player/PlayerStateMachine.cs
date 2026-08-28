using System.Linq.Expressions;
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
        Player.CurrentMovementDirection = Player.Controller.MovementDirection;

        if (Player.CurrentMovementDirection != Vector2.Zero)
        {
            StateMachine.ChangeState(new PlayerMovingState(Player, StateMachine));
        }
    }
}

public class PlayerMovingState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine)
{
    public override void PhysicsUpdate(double delta)
    {
        Player.NextMovementDirection = Player.Controller.MovementDirection;
        Player.Arrow.TurnArrow(Player.NextMovementDirection);
        var canMoveInDirection = Player.Arrow.CanMoveInDirection();

        GD.Print("current velocity before applying physics ", Player.Velocity);

        if (Player.CurrentMovementDirection == Vector2.Zero)
        {
            if (canMoveInDirection)
            {
                StateMachine.ChangeState(new PlayerIdleState(Player, stateMachine));
                return;
            }

            Player.CurrentMovementDirection = Player.NextMovementDirection;
        }
        if (canMoveInDirection)
        {
            Player.CurrentMovementDirection = Player.NextMovementDirection;
        }

        Player.Movement.ApplyVelocity(Player.CurrentMovementDirection);

        GD.Print("current velocity after applying physics ", Player.Velocity);

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
