using System.Collections.Generic;
using Game.UI.HUD;
using Godot;
using Utils;

namespace Game.UI;

public partial class UIManager : Node
{
    [Export]
    public CanvasLayer UIScreenContainer { get; set; }
    public static UIManager Instance { get; private set; }

    private Stack<UIScreen> Stack { get; init; } = [];

    public override void _Ready()
    {
        Instance = this;
    }

    public void Pop()
    {
        if (Stack.Count == 0)
        {
            return;
        }

        var top = Stack.Pop();

        top.OnPopped();
        top.QueueFree();

        if (Stack.Count > 0)
        {
            Stack.Peek().OnRevealed();
        }
    }

    public void Push(UIScreen screen)
    {
        if (Stack.Count > 0)
        {
            Stack.Peek().OnCovered();
        }

        UIScreenContainer.AddChild(screen);
        Stack.Push(screen);
        screen.Show();
        screen.OnPushed();
    }
}
