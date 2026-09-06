using System.Collections.Generic;

namespace Game.Common.AI;

public sealed class Selector<T>(params BehaviorNode<T>[] children) : BehaviorNode<T>
    where T : AIContext
{
    public List<BehaviorNode<T>> Children { get; init; } = [.. children];
    public int CurrentChildIndex { get; private set; } = 0;

    public override NodeStatus Tick(T context, double delta)
    {
        while (CurrentChildIndex < Children.Count)
        {
            var status = Children[CurrentChildIndex].Tick(context, delta);

            if (status == NodeStatus.Success)
            {
                CurrentChildIndex = 0;
                return NodeStatus.Success;
            }

            if (status == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }

            CurrentChildIndex++;
        }

        CurrentChildIndex = 0;
        return NodeStatus.Failure;
    }
}
