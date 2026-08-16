using Godot;

namespace Game.Resources.Movement;

[GlobalClass]
public partial class MovementData : Resource
{
    [Export]
    public float Speed { get; set; }

    [Export]
    public float Gravity { get; set; }
}
