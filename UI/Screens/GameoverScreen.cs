using Game.UI;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Game.Utilities.World.Maps;
using Godot;

namespace Utils;

public partial class GameoverScreen : UIScreen
{
    [Export]
    public Button RestartButton { get; set; }

    [Export]
    public Button MainMenuButton { get; set; }

    [Export]
    public Label Highscore { get; set; }

    [Export]
    public Label HighestLevel { get; set; }

    public override void _EnterTree()
    {
        // GD.Print($"GAMEOVER ENTER TREE {GetInstanceId()}");
    }

    public override void _Ready()
    {
        Highscore.Text = "Highscore: " + SaveManager.Instance.GameSaveState.Highscore.ToString();
        HighestLevel.Text =
            "Highest level: " + SaveManager.Instance.GameSaveState.HighestLevel.ToString();

        // GD.Print(
        //     $"GAMEOVER READY "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );
        RestartButton.Pressed += OnRestart;
        MainMenuButton.Pressed += OnMainMenu;
    }

    public override void _ExitTree()
    {
        // GD.Print($"GAMEOVER EXIT TREE {GetInstanceId()}");
        RestartButton.Pressed -= OnRestart;
        MainMenuButton.Pressed -= OnMainMenu;
    }

    public override void OnPushed()
    {
        GetTree().Paused = true;
    }

    public override void OnPopped() { }

    public void OnRestart()
    {
        UIManager.Instance.Pop();
        // SceneManager.Instance.ChangeScene<LevelManager>(LoadedScenes.LevelManager);
        GetTree().Paused = false;
        // GD.Print(
        //     $"Gameover restart level process mode: {ProcessMode}, "
        //         + $"tree paused: {GetTree().Paused}"
        // );
    }

    public async void OnMainMenu()
    {
        var tree = GetTree();
        var currentProcessMode = ProcessMode;

        UIManager.Instance.Pop();
        await TransitionManager.Instance.TransitionToScene<MainMenu>(LoadedScenes.MainMenu);
        tree.Paused = false;
        // SceneManager.Instance.ChangeScene<MainMenu>(LoadedScenes.MainMenu);
        GD.Print(
            $"Gameover go to main menu process mode: {currentProcessMode}, "
                + $"tree paused: {tree.Paused}"
        );
    }
}
