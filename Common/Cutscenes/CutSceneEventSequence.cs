using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

public partial class CutsceneSequence : CutSceneEvent
{
    [Export]
    public CutSceneEvent[] EventList { get; set; } = [];

    public override async Task Execute()
    {
        foreach (var cutsceneEvent in EventList)
        {
            await cutsceneEvent.Execute();
        }
    }
}
