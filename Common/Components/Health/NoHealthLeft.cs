using Game.Common.GameEvents;
using Godot;

namespace Game.Common.Components.Health;

public record NoHealthLeft : IGameEvent
{
    public Node Source { get; set; }
}
