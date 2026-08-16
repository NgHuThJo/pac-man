namespace Game.Common.GameEvents.Global;

public record ScoreChanged : IGameEvent
{
    public required int Score { get; init; }
}
