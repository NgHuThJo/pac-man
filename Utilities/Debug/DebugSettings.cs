using Godot;

namespace Game.Utilities.Debug;

public partial class DebugSettings : Node
{
    [Export]
    public bool GodMode { get; set; }

    [Export]
    public bool SuperSpeed { get; set; }
    public static DebugSettings Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}
