using Godot;

namespace Game.Utilities.Autoloads;

public partial class SceneManager : Node
{
    public Node CurrentScene { get; set; }

    public static SceneManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void ChangeScene<T>(PackedScene nextScene)
        where T : Node
    {
        // GD.Print(
        //     $"QUEUE FREE "
        //         + $"physics={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()}"
        // );
        CurrentScene?.QueueFree();

        // GD.Print(
        //     $"AFTER QUEUE FREE "
        //         + $"physics={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={CurrentScene?.IsQueuedForDeletion()}"
        // );
        CurrentScene = nextScene.Instantiate<T>();

        GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, CurrentScene);
    }
}
