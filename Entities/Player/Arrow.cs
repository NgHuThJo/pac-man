using System.Collections.Generic;
using Godot;

namespace Game.Entities.Player;

public partial class Arrow : Node2D
{
    [Export]
    public RayCast2D Ray { get; private set; }

    [Export]
    public Sprite2D Sprite { get; private set; }
    public const float RAY_LENGTH = 1000;

    public Dictionary<Vector2, float> RotationMap { get; init; } =
        new()
        {
            { Vector2.Right, 0 },
            { Vector2.Down, 90 },
            { Vector2.Left, 180 },
            { Vector2.Up, 270 },
        };

    public override void _Ready()
    {
        Ray.TargetPosition = new Vector2() { X = RAY_LENGTH, Y = 0 };
    }

    public void TurnArrow(Vector2 direction)
    {
        if (!RotationMap.TryGetValue(direction, out var value))
        {
            return;
        }

        RotationDegrees = value;
    }

    public bool CanMoveInDirection()
    {
        return !Ray.IsColliding();
    }
}
