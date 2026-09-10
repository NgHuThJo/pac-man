using System.Collections.Generic;
using System.Linq;
using Game.Common.Cutscenes;
using Game.Common.GameEvents.Global;
using Game.Common.Persistence;
using Game.Entities.Enemies;
using Game.Entities.Items;
using Game.Entities.Player;
using Game.UI;
using Game.UI.HUD;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Game.World.Maps;
using Godot;
using Utils;

namespace Game.Utilities.World.Maps;

public partial class Level : Node, ISaveable
{
    [Export]
    public Player Player { get; set; }

    [Export]
    public HUDManager HUD { get; set; }

    [Export]
    public TileMapLayer Map { get; set; }

    [Export]
    public AnimationPlayer AnimationPlayer { get; set; }

    [Export]
    public TeleportArea LeftArea { get; set; }

    [Export]
    public TeleportArea RightArea { get; set; }

    [Export]
    public CutsceneEventSequence Cutscene { get; set; }
    public int Score { get; private set; } = 0;
    public int CurrentLevel { get; private set; } = 1;
    public string CurrentLevelAnimation { get; set; } = "Start";
    public List<Vector2I> GateCoords { get; init; } = [new(14, 20), new(15, 20)];

    public override void _Ready()
    {
        var context = new CutsceneContext { Level = this };

        CutsceneManager.Instance.Play(Cutscene, context);

        Callable
            .From(() =>
            {
                foreach (var coin in GetTree().GetNodesInGroup("SmallCoins").Cast<SmallCoin>())
                {
                    coin.CoinCollected += OnCoinCollected;
                }
            })
            .CallDeferred();

        HUD.ShowHUD();
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

    public void ShowGameoverScreen()
    {
        SaveManager.Instance.Save();

        var instance = LoadedScenes.GameoverScreen.Instantiate<UIScreen>();
        UIManager.Instance.Push(instance);
        HUD.HideHUD();
    }

    public void OnCoinCollected(CoinCollected context)
    {
        IncreaseScore(context.Score);

        GD.Print("Currnet Score: ", Score);

        var newContext = new ScoreChanged() { Score = Score };

        EventBus.Instance.ScoreChanged?.Invoke(newContext);
    }

    public void Save()
    {
        SaveManager.Instance.GameSaveState.Highscore = Mathf.Max(
            SaveManager.Instance.GameSaveState.Highscore,
            Score
        );
        SaveManager.Instance.GameSaveState.HighestLevel = Mathf.Max(
            SaveManager.Instance.GameSaveState.HighestLevel,
            CurrentLevel
        );
    }
}
