using Game.Resources.Movement;
using Godot;

namespace Game.Common.Components;

public partial class MovementComponent : Node
{
    [Export]
    private CharacterBody2D _body;

    private MovementData _data;
    public Vector2 Velocity
    {
        get => _body.Velocity;
        set => _body.Velocity = value;
    }

    public void Initialize(MovementData data)
    {
        _data = data;
    }

    public bool IsGrounded()
    {
        return _body.IsOnFloor();
    }

    public void ApplyVelocity(Vector2 direction)
    {
        Velocity = direction * _data.Speed;
    }

    public void ApplyHorizontalVelocity(float directionX)
    {
        Velocity = Velocity with { X = directionX * _data.Speed };
    }

    public void ApplyVerticalVelocity(float directionY)
    {
        Velocity = Velocity with { Y = directionY * _data.Speed };
    }

    public void ApplyGravity(double delta)
    {
        if (!IsGrounded())
        {
            Velocity += Vector2.Down * _data.Gravity * (float)delta;
        }
    }

    public void Accelerate(Vector2 direction, float acceleration, double delta)
    {
        var currentAcceleration = acceleration * (float)delta;

        Velocity = Velocity with
        {
            X =
                direction.X != 0
                    ? Mathf.MoveToward(Velocity.X, _data.Speed * 1.5f, currentAcceleration)
                    : Velocity.X,
            Y =
                direction.Y != 0
                    ? Mathf.MoveToward(Velocity.Y, _data.Speed * 1.5f, currentAcceleration)
                    : Velocity.Y,
        };
    }

    public void Decelerate(Vector2 direction, float deceleration, double delta)
    {
        var currentDeceleration = deceleration * (float)delta;

        Velocity = Velocity with
        {
            X =
                direction.X != 0
                    ? Mathf.MoveToward(Velocity.X, _data.Speed * 0.5f, currentDeceleration)
                    : Velocity.X,
            Y =
                direction.Y != 0
                    ? Mathf.MoveToward(Velocity.Y, _data.Speed * 0.5f, currentDeceleration)
                    : Velocity.Y,
        };
    }
}
