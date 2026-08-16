using System;
using Game.Common.Components.AreaBoxes.Hitbox;
using Godot;

namespace Game.Common.Components.AreaBoxes.Hurtbox;

public partial class HurtboxComponent : Area2D
{
    public event Action<HitReceived> HitReceived;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public override void _ExitTree()
    {
        AreaEntered -= OnAreaEntered;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent hitbox)
        {
            var context = new HitReceived { Hitter = hitbox };

            HitReceived?.Invoke(context);
        }
    }
}
