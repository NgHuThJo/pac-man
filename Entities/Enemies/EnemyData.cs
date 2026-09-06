using Game.Resources.Movement;
using Godot;

namespace Game.Entities.Enemies;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export]
    public MovementData MovementData { get; private set; }

    [Export]
    public Vector2I ScatterPosition { get; private set; }

    [Export]
    public int Points { get; private set; }
}
