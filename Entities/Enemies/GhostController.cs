using System.Collections.Generic;
using Godot;

namespace Game.Entities.Enemies;

public partial class GhostController : Node
{
    [Export]
    private Timer ModeTimer { get; set; }
    private List<GhostModeData> Sequence { get; init; } =
    [
        new(GhostMode.Scatter, 7f),
        new(GhostMode.Chase, 10f),
        new(GhostMode.Scatter, 7f),
        new(GhostMode.Chase, 10f),
    ];
    private int CurrentIndex { get; set; } = 0;

    public GhostMode CurrentMode { get; private set; }

    public override void _Ready()
    {
        ModeTimer.Timeout += OnTimeout;
        ExecuteStep();
    }

    public override void _ExitTree()
    {
        ModeTimer.Timeout -= OnTimeout;
    }

    private void ExecuteStep()
    {
        if (CurrentIndex >= Sequence.Count)
        {
            return;
        }

        var step = Sequence[CurrentIndex];
        CurrentIndex++;
        ModeTimer.WaitTime = step.Duration;
        CurrentMode = step.Mode;
        ModeTimer.Start();

        GD.Print("Current mode: ", CurrentMode);
    }

    private void OnTimeout()
    {
        ExecuteStep();
    }
}
