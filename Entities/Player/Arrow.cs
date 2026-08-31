using Godot;

namespace Game.Entities.Player;

public partial class Arrow : Node2D
{
    [Export]
    public Sprite2D Sprite { get; private set; }
}
