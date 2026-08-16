using Game.Common.GameEvents;

namespace Game.UI.HUD;

public record LevelChanged : IGameEvent
{
    public int CurrentLevel { get; init; }
}
