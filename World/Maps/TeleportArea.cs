using Game.Entities.Player;
using Godot;

namespace Game.World.Maps;

public partial class TeleportArea : Area2D
{
    [Export]
    public TeleportArea Destination { get; set; }

    [Export]
    public Marker2D SpawnPosition { get; set; }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _ExitTree()
    {
        BodyEntered -= OnBodyEntered;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.GlobalPosition = Destination.SpawnPosition.GlobalPosition;
            GD.Print("Player position changed, ", player.GlobalPosition);
        }
    }
}
