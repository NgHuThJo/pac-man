using Game.Common.Cutscenes;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class CutsceneManager : Node
{
    public static CutsceneManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public async void Play(CutsceneEventSequence sequence, CutsceneContext context)
    {
        try
        {
            InputManager.Instance.Push(InputState.Cutscene);
            await sequence.Execute(context);
        }
        finally
        {
            GD.Print("player start");
            InputManager.Instance.Pop();
        }
    }
}
