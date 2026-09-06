using Game.Common.Components;
using Godot;

namespace Game.Entities.Enemies;

public partial class Ghost : CharacterBody2D
{
    [Export]
    public NavigationAgent2D Navigation { get; set; }

    [Export]
    public MovementComponent Movement { get; set; }

    [Export]
    public Player.Player Player { get; set; }

    [Export]
    public EnemyData Data { get; set; }

    public override void _Ready()
    {
        Callable.From(SetupActor).CallDeferred();
        Initialize();
    }

    public virtual void Initialize()
    {
        Movement.Initialize(Data.MovementData);
    }

    private async void SetupActor()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        Navigation.TargetPosition = Player.GlobalPosition;
    }
}
