using Game.Common.AI;

namespace Game.Entities.Enemies;

public sealed class EnemyBehaviorContext : AIContext
{
    public required Player.Player Player { get; init; }
    public required Ghost Ghost { get; init; }
    public required GhostMode CurrentGhostMode { get; set; }
}
