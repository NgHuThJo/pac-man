using Game.Common.GameEvents.Global;
using Game.Common.Persistence;
using Game.Entities.Enemies;
using Game.Entities.Player;
using Game.UI;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Godot;
using Utils;

namespace Game.Utilities.World.Maps;

public partial class LevelManager : Node, ISaveable
{
    [Export]
    public Player Player { get; set; }

    public int Score { get; private set; } = 0;
    public int Level { get; private set; } = 1;

    public override void _EnterTree()
    {
        Player.SetSpawnContainer(this);
    }

    public override void _Ready()
    {
        UIManager.Instance.HUDManager.ResetHUD(Score, Level);
        UIManager.Instance.HUDManager.ShowHUD();

        EventBus.Instance.EnemyDied += OnEnemyDied;
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print(
        //     $"LEVEL MANAGER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );
    }

    public override void _ExitTree()
    {
        EventBus.Instance.EnemyDied -= OnEnemyDied;
    }

    public void OnEnemyDied(EnemyDied context)
    {
        IncreaseScore(context.Points);

        var currentContext = new ScoreChanged { Score = Score };

        EventBus.Instance.ScoreChanged.Invoke(currentContext);
    }

    public void IncreaseScore(int score)
    {
        Score += score;
    }

    public void IncrementLevel()
    {
        Level++;
    }

    public void ShowGameoverScreen()
    {
        SaveManager.Instance.Save();

        var instance = LoadedScenes.GameoverScreen.Instantiate<UIScreen>();
        UIManager.Instance.Push(instance);
        UIManager.Instance.HUDManager.Hide();
    }

    public void Save()
    {
        SaveManager.Instance.GameSaveState.Highscore = Mathf.Max(
            SaveManager.Instance.GameSaveState.Highscore,
            Score
        );
        SaveManager.Instance.GameSaveState.HighestLevel = Mathf.Max(
            SaveManager.Instance.GameSaveState.HighestLevel,
            Level
        );
    }
}
