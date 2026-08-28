using System.Collections.Generic;
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
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public RayCast2D Ray { get; private set; }

    [Export]
    public Arrow Arrow { get; private set; }

    [Export]
    public PlayerData Data { get; private set; }
    public PlayerStateMachine StateMachine { get; init; } = new();
    public Vector2 NextMovementDirection { get; set; } = Vector2.Zero;
    public Vector2 CurrentMovementDirection { get; set; } = Vector2.Zero;

    public override void _Ready()
    {
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

        StateMachine.PhysicsUpdate(delta);
    }

    public void Initialize()
    {
        Health.Initialize(Data.HealthData);
        Movement.Initialize(Data.MovementData);
    }
}
