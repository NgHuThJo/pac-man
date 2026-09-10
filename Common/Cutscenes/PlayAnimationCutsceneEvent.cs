using System.Threading.Tasks;
using Godot;

namespace Game.Common.Cutscenes;

[GlobalClass]
public partial class PlayAnimationCutsceneEvent : CutsceneEvent
{
    public override async Task Execute(CutsceneContext context)
    {
        context.Level.AnimationPlayer.Play(context.Level.CurrentLevelAnimation);

        await ToSignal(context.Level.AnimationPlayer, AnimationMixer.SignalName.AnimationFinished);
    }
}
