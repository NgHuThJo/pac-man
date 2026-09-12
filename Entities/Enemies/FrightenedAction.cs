using System;
using Game.Common.AI;
using Godot;

namespace Game.Entities.Enemies;

public sealed class FrightenedAction : BehaviorNode<EnemyBehaviorContext>
{
    public override NodeStatus Tick(EnemyBehaviorContext context, double delta)
    {
        if (context.CurrentGhostMode != GhostMode.Frightened)
        {
            return NodeStatus.Failure;
        }

        var randomNumber = GD.Randf();
        var nextDirection = Vector2.Zero;

        if (randomNumber < 0.25)
        {
            nextDirection = Vector2.Up;
        }
        else if (randomNumber < 0.5)
        {
            nextDirection = Vector2.Right;
        }
        else if (randomNumber < 0.75)
        {
            nextDirection = Vector2.Down;
        }
        else
        {
            nextDirection = Vector2.Left;
        }

        context.Ghost.Movement.ApplyVelocity(nextDirection);

        context.Ghost.MoveAndSlide();

        return NodeStatus.Running;
    }
}
