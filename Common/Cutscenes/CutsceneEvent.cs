using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

public abstract partial class CutsceneEvent : Resource
{
    public abstract Task Execute(CutsceneContext context);
}
