using Game.Common.AI;
using Godot;

namespace Game.Entities.Enemies;

public partial class RedGhost : Ghost
{
    [Export]
    public GhostController Controller { get; set; }
    private BehaviorNode<EnemyBehaviorContext> BehaviorTree { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Initialize();
    }

    public override void _PhysicsProcess(double delta)
    {
        BehaviorTree.Tick(
            new EnemyBehaviorContext()
            {
                Ghost = this,
                Player = Player,
                CurrentGhostMode = Controller.CurrentMode,
            },
            delta
        );
    }

    public override void Initialize()
    {
        base.Initialize();
        BehaviorTree = new Selector<EnemyBehaviorContext>(new ScatterAction(), new ChaseAction());
    }
}
