using Godot;

namespace Game.Utilities.Autoloads;

public partial class EffectManager : CanvasLayer
{
    public static EffectManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public T Spawn<T>(PackedScene effectScene, Vector2 globalPosition)
        where T : Node2D
    {
        var instance = effectScene.Instantiate<T>();

        AddChild(instance);

        instance.GlobalPosition = globalPosition;

        return instance;
    }
}
