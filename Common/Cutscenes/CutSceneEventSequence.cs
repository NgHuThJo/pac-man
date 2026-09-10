using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

[GlobalClass]
public partial class CutsceneEventSequence : CutsceneEvent
{
    [Export]
    public CutsceneEvent[] EventList { get; set; } = [];

    public override async Task Execute(CutsceneContext context)
    {
        foreach (var cutsceneEvent in EventList)
        {
            await cutsceneEvent.Execute(context);
        }
    }
}
