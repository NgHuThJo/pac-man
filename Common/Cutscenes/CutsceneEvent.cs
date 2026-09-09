using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

public abstract partial class CutSceneEvent : Resource
{
    public abstract Task Execute();
}
