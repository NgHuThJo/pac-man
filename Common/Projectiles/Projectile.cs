using Game.Common.Components.AreaBoxes.Hitbox;
using Game.Resources.Attack;
using Godot;

namespace Game.Common.Projectiles;

public partial class Projectile : CharacterBody2D
{
    [Export]
    public Timer Lifetime { get; set; }

    [Export]
    public HitboxComponent Hitbox { get; set; }

    [Export]
    public VisibleOnScreenEnabler2D ScreenEnabler { get; set; }

    [Export]
    public Vector2 Direction { get; set; }
    public AttackData Data { get; set; }

    public override void _Ready()
    {
        Lifetime.Timeout += OnTimeout;
        Hitbox.AreaEntered += OnAreaEntered;
        ScreenEnabler.ScreenExited += OnScreenExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Direction * Data.ProjectileSpeed;
        MoveAndSlide();
    }

    public override void _ExitTree()
    {
        Lifetime.Timeout -= OnTimeout;
        Hitbox.AreaEntered -= OnAreaEntered;
        ScreenEnabler.ScreenExited -= OnScreenExited;
    }

    public virtual void Initialize(AttackData data)
    {
        Data = data;
        Lifetime.WaitTime = data.ProjectileLifetime;
        Hitbox.Initialize(data);
    }

    public void OnTimeout()
    {
        QueueFree();
    }

    public void OnAreaEntered(Area2D _)
    {
        QueueFree();
    }

    public void OnScreenExited()
    {
        QueueFree();
    }
}
