using Godot;

namespace Game.Common.Controllers.Character;

public abstract partial class CharacterController : Node
{
    public abstract Vector2 MovementDirection { get; }
}
