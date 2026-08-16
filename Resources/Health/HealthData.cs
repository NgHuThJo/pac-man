using Godot;

namespace Game.Resources.Health;

[GlobalClass]
public partial class HealthData : Resource
{
    [Export]
    public float MaxHealth { get; set; }
}
