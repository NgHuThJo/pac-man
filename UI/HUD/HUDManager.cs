using Godot;
using Utils;

namespace Game.UI.HUD;

public partial class HUDManager : CanvasLayer
{
    [Export]
    public ScoreDisplay ScoreDisplay { get; private set; }

    [Export]
    public LevelDisplay LevelDisplay { get; private set; }

    public override void _Ready()
    {
        HideHUD();
    }

    public void ShowHUD()
    {
        Show();
    }

    public void HideHUD()
    {
        Hide();
    }

    public void ResetHUD(int score, int level)
    {
        ScoreDisplay.SetScore(score);
        LevelDisplay.SetLevel(level);
    }
}
