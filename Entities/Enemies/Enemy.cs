using Game.Common.Components;
using Game.Common.Components.AreaBoxes.Hurtbox;
using Game.Common.Components.Health;
using Game.Common.Particles;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Godot;

namespace Game.Entities.Enemies;

public partial class Enemy : CharacterBody2D
{
    [Export]
    public MovementComponent Movement { get; private set; }

    [Export]
    public HealthComponent Health { get; private set; }

    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public EnemyData Data { get; set; }

    private bool HasDied { get; set; } = false;

    public override void _Ready()
    {
        Movement.Initialize(Data.MovementData);

        Health.NoHealthLeft += OnNoHealthLeft;
    }

    public override void _ExitTree()
    {
        Health.NoHealthLeft -= OnNoHealthLeft;
    }

    public async void OnNoHealthLeft(NoHealthLeft noHealthLeft)
    {
        if (HasDied || noHealthLeft.Source is not Enemy)
        {
            return;
        }

        HasDied = true;

        // EffectManager.Instance.Spawn<Explosion>(LoadedVfx.Explosion, GlobalPosition);
        // AudioManager.Instance.PlaySfx(LoadedSfx.Explosion);

        QueueFree();
        var context = new EnemyDied { Points = Data.Points };
        EventBus.Instance.EnemyDied.Invoke(context);
    }
}
