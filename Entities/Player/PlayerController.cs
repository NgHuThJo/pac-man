using Game.Common.Controllers.Character;
using Godot;

namespace Game.Entities.Player;

public partial class PlayerController : CharacterController
{
    public override Vector2 MovementDirection
    {
        get
        {
            Vector2 direction = Vector2.Zero;

            if (Input.IsActionPressed("move_left"))
            {
                direction = Vector2.Left;
            }
            else if (Input.IsActionPressed("move_right"))
            {
                direction = Vector2.Right;
            }
            else if (Input.IsActionPressed("move_up"))
            {
                direction = Vector2.Up;
            }
            else if (Input.IsActionPressed("move_down"))
            {
                direction = Vector2.Down;
            }

            return direction;
        }
    }
}
