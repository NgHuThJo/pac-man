using Game.Common.AI;

namespace Game.Entities.Enemies;

public sealed class ScatterAction : BehaviorNode<EnemyBehaviorContext>
{
    public override NodeStatus Tick(EnemyBehaviorContext context, double delta)
    {
        if (context.CurrentGhostMode != GhostMode.Scatter)
        {
            return NodeStatus.Failure;
        }

        context.Ghost.Navigation.TargetPosition = context.Ghost.Data.ScatterPosition;
        var currentPosition = context.Ghost.GlobalTransform.Origin;
        var nextPosition = context.Ghost.Navigation.GetNextPathPosition();
        context.Ghost.Movement.ApplyVelocity(currentPosition.DirectionTo(nextPosition));

        context.Ghost.MoveAndSlide();

        return NodeStatus.Running;
    }
}
