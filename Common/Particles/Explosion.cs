using Godot;

namespace Game.Common.Particles;

public partial class Explosion : Node2D
{
    [Export]
    private GpuParticles2D Particles { get; set; }

    public override void _Ready()
    {
        Particles.Finished += QueueFree;

        Particles.OneShot = true;
        Particles.Emitting = true;
    }
}
