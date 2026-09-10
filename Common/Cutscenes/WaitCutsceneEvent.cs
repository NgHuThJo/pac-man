using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

[GlobalClass]
public partial class WaitCutsceneEvent : CutsceneEvent
{
    public override async Task Execute(CutsceneContext context)
    {
        var tree = context.Level.GetTree();

        await ToSignal(tree.CreateTimer(5), SceneTreeTimer.SignalName.Timeout);
    }
}
