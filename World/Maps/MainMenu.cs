using Game.UI;
using Game.UI.Settings;
using Game.Utilities.Loaded;
using Godot;
using Utils;

namespace Game.Utilities.World.Maps;

public partial class MainMenu : UIScreen
{
    [Export]
    public Button StartButton { get; set; }

    [Export]
    public Button SettingsButton { get; set; }

    [Export]
    public Button ExitButton { get; set; }

    public override void _Ready()
    {
        StartButton.Pressed += OnStartPressed;
        SettingsButton.Pressed += OnSettingsPressed;
        ExitButton.Pressed += OnExitPressed;
    }

    public override void _ExitTree()
    {
        StartButton.Pressed -= OnStartPressed;
        SettingsButton.Pressed -= OnSettingsPressed;
        ExitButton.Pressed -= OnExitPressed;
    }

    public async void OnStartPressed()
    {
        // await TransitionManager.Instance.TransitionToScene<LevelManager>(LoadedScenes.LevelManager);
    }

    public async void OnSettingsPressed()
    {
        var instance = LoadedScenes.Settings.Instantiate<Settings>();
        UIManager.Instance.Push(instance);
    }

    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}
