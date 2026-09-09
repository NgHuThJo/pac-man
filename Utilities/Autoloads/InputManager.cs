using System.Collections.Generic;
using Godot;

namespace Game.Utilities.Autoloads;

public enum InputState
{
    None,
    Player,
    Cutscene,
}

public partial class InputManager : Node
{
    public static InputManager Instance { get; private set; }
    private Stack<InputState> Stack { get; init; } = [];

    public override void _Ready()
    {
        Instance = this;
    }

    public void Push(InputState newInputState)
    {
        Stack.Push(newInputState);
    }

    public void Pop()
    {
        if (Stack.Count == 0)
        {
            GD.PushError($"No element left in input state stack");
            return;
        }

        Stack.Pop();
    }
}
