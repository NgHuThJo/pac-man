using System;
using Game.Common.Components.AreaBoxes.Hurtbox;
using Game.Resources.Attack;
using Godot;

namespace Game.Common.Components.AreaBoxes.Hitbox;

public partial class HitboxComponent : Area2D
{
    public event Action<HitApplied> HitApplied;
    public AttackData AttackData { get; set; }

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public override void _ExitTree()
    {
        AreaEntered -= OnAreaEntered;
    }

    public void Initialize(AttackData data)
    {
        AttackData = data;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HurtboxComponent hurtbox)
        {
            var context = new HitApplied { Attack = AttackData };

            HitApplied?.Invoke(context);
        }
    }
}
