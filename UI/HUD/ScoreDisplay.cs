using Game.Common.GameEvents.Global;
using Game.Utilities.Autoloads;
using Godot;

namespace Utils;

public partial class ScoreDisplay : Control
{
    [Export]
    public Label ScoreLabel { get; set; }

    public override void _Ready()
    {
        EventBus.Instance.ScoreChanged += OnScoreChanged;
        ScoreLabel.Text = 0.ToString();
    }

    public override void _ExitTree()
    {
        EventBus.Instance.ScoreChanged -= OnScoreChanged;
    }

    public void SetScore(int score)
    {
        ScoreLabel.Text = score.ToString();
    }

    public void OnScoreChanged(ScoreChanged context)
    {
        ScoreLabel.Text = context.Score.ToString();
        GD.Print("Score label updated, ", ScoreLabel.Text);
    }
}
