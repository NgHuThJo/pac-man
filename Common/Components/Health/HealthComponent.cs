using System;
using Game.Common.Components.AreaBoxes.Hurtbox;
using Game.Resources.Health;
using Godot;

namespace Game.Common.Components.Health;

public partial class HealthComponent : Node
{
    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    public event Action<HealthChanged> HealthChanged;
    public event Action<NoHealthLeft> NoHealthLeft;
    public HealthData Data { get; private set; }
    public float CurrentHealth { get; private set; }

    public override void _Ready()
    {
        Hurtbox.HitReceived += OnHitReceived;
    }

    public override void _ExitTree()
    {
        Hurtbox.HitReceived -= OnHitReceived;
    }

    public void Initialize(HealthData data)
    {
        Data = data;
        CurrentHealth = data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Data.MaxHealth);
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void OnHitReceived(HitReceived context)
    {
        TakeDamage(context.Hitter.AttackData.Damage);

        var healthChangedContext = new HealthChanged() { CurrentHealth = CurrentHealth };

        HealthChanged?.Invoke(healthChangedContext);

        if (IsDead())
        {
            var noHealthLeftContext = new NoHealthLeft { Source = GetParent() };

            NoHealthLeft?.Invoke(noHealthLeftContext);
        }
    }
}
