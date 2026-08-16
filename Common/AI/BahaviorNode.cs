namespace Game.Common.AI;

public enum NodeStatus
{
    Failure,
    Success,
    Running,
};

public abstract class BehaviorNode
{
    public abstract void Tick(AIContext context);
}
