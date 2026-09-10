using System;
using System.Collections.Generic;
using Godot;

namespace Game.Utilities.Autoloads;

public enum InputState
{
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
        Push(InputState.Player);
    }

    public InputState Current
    {
        get
        {
            if (!Stack.TryPeek(out var inputState))
            {
                throw new InvalidOperationException("Input state stack is empty");
            }

            return inputState;
        }
    }

    public void Push(InputState newInputState)
    {
        Stack.Push(newInputState);
    }

    public void Pop()
    {
        if (Stack.Count == 0)
        {
            GD.PushError("No element to pop in input state stack");
            return;
        }

        Stack.Pop();
    }
}
