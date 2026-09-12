using Game.Common.AI;
using Godot;

namespace Game.Entities.Enemies;

public sealed class SurroundPlayerAction : BehaviorNode<EnemyBehaviorContext>
{
    public override NodeStatus Tick(EnemyBehaviorContext context, double delta)
    {
        if (context.CurrentGhostMode != GhostMode.Chase)
        {
            return NodeStatus.Failure;
        }

        var playerPosition = context.Player.GlobalPosition;
        var scatterPosition = context.Ghost.Data.ScatterPosition;
        var ghostPosition = context.Ghost.GlobalPosition;

        if (ghostPosition.DistanceTo(playerPosition) < 128)
        {
            context.Ghost.Navigation.TargetPosition = scatterPosition;
        }
        else
        {
            context.Ghost.Navigation.TargetPosition = playerPosition;
        }

        var currentAgentPosition = context.Ghost.GlobalTransform.Origin;
        var nextPosition = context.Ghost.Navigation.GetNextPathPosition();

        context.Ghost.Movement.ApplyVelocity(currentAgentPosition.DirectionTo(nextPosition));
        context.Ghost.MoveAndSlide();

        if (context.Ghost.Navigation.IsNavigationFinished())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
