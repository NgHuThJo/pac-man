using Godot;

namespace Game.Entities.Items;

public partial class SmallCoin : StaticBody2D
{
    [Export]
    public Area2D DetectionBox { get; private set; }

    [Export]
    public AudioStreamPlayer Sfx { get; private set; }

    public override void _Ready()
    {
        DetectionBox.BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D area)
    {
        Sfx.Play();
        QueueFree();
    }
}
