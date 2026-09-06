namespace Game.Common.AI;

public enum NodeStatus
{
    Failure,
    Success,
    Running,
};

public abstract class BehaviorNode<T>
    where T : AIContext
{
    public abstract NodeStatus Tick(T context, double delta);
}
