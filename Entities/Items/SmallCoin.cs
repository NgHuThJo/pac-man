using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Godot;

namespace Game.Entities.Items;

public partial class SmallCoin : StaticBody2D
{
    [Export]
    public Area2D DetectionBox { get; private set; }
    public AudioStreamWav[] SfxList { get; init; } = [LoadedSfx.Eat1, LoadedSfx.Eat2];
    private int CurrentIndex { get; set; } = 0;

    public override void _Ready()
    {
        DetectionBox.BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D area)
    {
        AudioManager.Instance.PlaySfx(SfxList[CurrentIndex % SfxList.Length]);
        CurrentIndex++;
        QueueFree();
    }
}
