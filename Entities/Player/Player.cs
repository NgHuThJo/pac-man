using Game.Common.Components;
using Game.Common.Components.AreaBoxes.Hurtbox;
using Game.Common.Components.Health;
using Godot;

namespace Game.Entities.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerController Controller { get; private set; }

    [Export]
    public HealthComponent Health { get; private set; }

    [Export]
    public MovementComponent Movement { get; private set; }

    [Export]
    public AttackComponent Attack { get; private set; }

    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public PlayerData Data { get; private set; }
    public PlayerStateMachine StateMachine { get; init; } = new();
    private Node SpawnContainer { get; set; }

    // public override void _EnterTree()
    // {
    //     GD.Print(
    //         $"PLAYER ENTER TREE "
    //             + $"physics={Engine.GetPhysicsFrames()} "
    //             + $"process={Engine.GetProcessFrames()}"
    //     );
    // }

    public override void _Ready()
    {
        // GD.Print($"PLAYER READY {GetInstanceId()}");
        Initialize();

        StateMachine.ChangeState(new PlayerMovingState(this, StateMachine));
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print(
        //     $"PLAYER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );

        var direction = Controller.MovementDirection;
        Movement.ApplyHorizontalVelocity(direction.X);
        Movement.ApplyGravity(delta);
        MoveAndSlide();

        StateMachine.Update(delta);
    }

    public override void _ExitTree()
    {
        // GD.Print(
        //     $"PLAYER EXIT "
        //         + $"physics={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()}"
        // );
    }

    public void Initialize()
    {
        Health.Initialize(Data.HealthData);
        Movement.Initialize(Data.MovementData);
        Attack.Initialize(Data.AttackData, SpawnContainer);
    }

    public void SetSpawnContainer(Node spawnContainer)
    {
        SpawnContainer = spawnContainer;
    }
}
