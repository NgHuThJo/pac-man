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

    public static async void PlayCutscene(CutsceneSequence sequence)
    {
        await sequence.Execute();
    }
}
