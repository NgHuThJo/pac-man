using Game.Resources.Health;
using Game.Resources.Movement;
using Godot;

namespace Game.Entities.Player;

[GlobalClass]
public partial class PlayerData : Resource
{
    [Export]
    public HealthData HealthData { get; set; }

    [Export]
    public MovementData MovementData { get; set; }
}
