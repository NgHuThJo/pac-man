using Game.Common.GameEvents;

namespace Game.Common.Components.Health;

public record HealthChanged : IGameEvent
{
    public required float CurrentHealth { get; init; }
}
