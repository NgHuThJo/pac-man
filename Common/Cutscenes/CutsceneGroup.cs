using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

public partial class CutsceneGroup : CutsceneEvent
{
    [Export]
    public CutsceneEvent[] Cutscenes { get; set; }

    public override async Task Execute(CutsceneContext context)
    {
        var tasks = Cutscenes.Select(async cutscene => await cutscene.Execute(context));

        await Task.WhenAll(tasks);
    }
}
