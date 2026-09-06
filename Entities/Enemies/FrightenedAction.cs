using Game.Common.AI;

namespace Game.Entities.Enemies;

public sealed class FrightenedAction : BehaviorNode<EnemyBehaviorContext>
{
    public override NodeStatus Tick(EnemyBehaviorContext context, double delta)
    {
        if (context.CurrentGhostMode != GhostMode.Frightened)
        {
            return NodeStatus.Failure;
        }

        return NodeStatus.Running;
    }
}
