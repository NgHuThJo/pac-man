using System;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Godot;

namespace Game.Entities.Items;

public partial class BigCoin : StaticBody2D
{
    public event Action<CoinCollected> BigCoinCollected;

    [Export]
    public Area2D DetectionBox { get; private set; }

    [Export]
    public ItemData Data { get; private set; }
    public AudioStreamWav[] SfxList { get; init; } = [LoadedSfx.Eat1, LoadedSfx.Eat2];
    private int CurrentIndex { get; set; } = 0;

    public override void _Ready()
    {
        DetectionBox.BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player.Player player)
        {
            AudioManager.Instance.PlaySfx(SfxList[CurrentIndex % SfxList.Length]);
            CurrentIndex++;
            QueueFree();

            var context = new CoinCollected() { Score = Data.Points };
            BigCoinCollected?.Invoke(context);
        }
    }
}
