using Game.Common.Components.AreaBoxes.Hitbox;
using Game.Common.GameEvents;

namespace Game.Common.Components.AreaBoxes.Hurtbox;

public record HitReceived : IGameEvent
{
    public HitboxComponent Hitter;
}
