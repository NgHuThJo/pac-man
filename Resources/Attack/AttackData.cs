using Godot;

namespace Game.Resources.Attack;

[GlobalClass]
public partial class AttackData : Resource
{
    [Export]
    public float Damage { get; set; }

    [Export]
    public float Cooldown { get; set; }

    [Export]
    public float ProjectileSpeed { get; set; }

    [Export]
    public float ProjectileLifetime { get; set; }

    [Export]
    public PackedScene ProjectileScene { get; set; }
}
