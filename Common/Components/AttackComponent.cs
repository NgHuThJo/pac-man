using Game.Resources.Attack;
using Godot;

namespace Game.Common.Components;

public partial class AttackComponent : Node2D
{
    [Export]
    public Timer Cooldown { get; private set; }

    [Export]
    public Marker2D Muzzle { get; private set; }

    [Export]
    public AudioStreamPlayer Sfx { get; private set; }
    public AttackData Data { get; private set; }
    private Node SpawnContainer { get; set; }

    public void Initialize(AttackData data, Node spawnContainer)
    {
        Data = data;
        Cooldown.WaitTime = Data.Cooldown;
        SpawnContainer = spawnContainer;
    }

    public void Attack()
    {
        if (!Cooldown.IsStopped())
        {
            return;
        }

        Sfx.Play();

        Cooldown.Start();
    }
}
