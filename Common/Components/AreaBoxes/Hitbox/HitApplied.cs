using Game.Common.GameEvents;
using Game.Resources.Attack;

namespace Game.Common.Components.AreaBoxes.Hitbox;

public record HitApplied : IGameEvent
{
    public AttackData Attack { get; init; }
}
