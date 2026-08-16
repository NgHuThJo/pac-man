using Game.Utilities.Autoloads;
using Godot;

namespace Game.UI.HUD;

public partial class LevelDisplay : PanelContainer
{
    [Export]
    public Label LevelLabel { get; set; }

    public override void _Ready()
    {
        EventBus.Instance.LevelChanged += OnLevelChanged;

        LevelLabel.Text = 1.ToString();
    }

    public override void _ExitTree()
    {
        EventBus.Instance.LevelChanged -= OnLevelChanged;
    }

    public void SetLevel(int level)
    {
        LevelLabel.Text = level.ToString();
    }

    public void OnLevelChanged(LevelChanged context)
    {
        LevelLabel.Text = context.CurrentLevel.ToString();
    }
}
