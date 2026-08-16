using Game.Common.Controllers.Character;
using Godot;

namespace Game.Entities.Player;

public partial class PlayerController : CharacterController
{
    public override Vector2 MovementDirection
    {
        get
        {
            var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

            return new() { X = direction.X, Y = direction.Y };
        }
    }
}
