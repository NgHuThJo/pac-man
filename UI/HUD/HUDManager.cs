using Godot;
using Utils;

namespace Game.UI.HUD;

public partial class HUDManager : CanvasLayer
{
    [Export]
    public ScoreDisplay ScoreDisplay { get; private set; }

    public override void _Ready() { }

    public void ShowHUD()
    {
        Show();
    }

    public void HideHUD()
    {
        Hide();
    }

    public void ResetHUD(int score)
    {
        ScoreDisplay.SetScore(score);
    }
}
