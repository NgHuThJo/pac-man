using System.Threading.Tasks;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class TransitionManager : CanvasLayer
{
    [Export]
    public ColorRect Background { get; set; }

    [Export]
    private float TransitionDuration { get; set; } = 0.5f;

    public static TransitionManager Instance { get; private set; }
    public bool IsTransitioning { get; set; } = false;

    public override void _Ready()
    {
        Instance = this;
    }

    public async Task TransitionToScene<T>(PackedScene scene)
        where T : Node
    {
        if (IsTransitioning)
        {
            return;
        }

        IsTransitioning = true;

        // Background must be turned visible at start
        Background.Visible = true;
        var tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(Background, "modulate:a", 1, TransitionDuration);
        await ToSignal(tween, Tween.SignalName.Finished);

        SceneManager.Instance.ChangeScene<T>(scene);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(Background, "modulate:a", 0, TransitionDuration);
        await ToSignal(tween, Tween.SignalName.Finished);
        // Background must be turned invisible at end
        Background.Visible = false;

        IsTransitioning = false;
    }
}
