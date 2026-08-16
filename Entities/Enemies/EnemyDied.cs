using Game.Common.GameEvents;

namespace Game.Entities.Enemies;

public record EnemyDied : IGameEvent
{
    public int Points { get; init; }
}
