using Game.Common.GameEvents;

namespace Game.Entities.Items;

public record CoinCollected : IGameEvent
{
    public int Score { get; init; }
}
