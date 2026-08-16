using System;
using Game.Common.GameEvents.Global;
using Game.Entities.Enemies;
using Game.UI.HUD;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class EventBus : Node
{
    public Action<EnemyDied> EnemyDied;
    public Action<ScoreChanged> ScoreChanged;
    public Action<LevelChanged> LevelChanged;
    public static EventBus Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}
