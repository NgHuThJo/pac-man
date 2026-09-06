namespace Game.Entities.Enemies;

public enum GhostMode
{
    Scatter,
    Chase,
    Frightened,
}

public record GhostModeData(GhostMode Mode, float Duration) { }
