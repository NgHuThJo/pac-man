using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

[GlobalClass]
public partial class OpenGateCutsceneEvent : CutsceneEvent
{
    public override async Task Execute(CutsceneContext context)
    {
        var level = context.Level;

        foreach (var cellCoord in level.GateCoords)
        {
            level.Map.SetCell(cellCoord);
            GD.Print($"Cell {cellCoord} deleted");
        }
    }
}
