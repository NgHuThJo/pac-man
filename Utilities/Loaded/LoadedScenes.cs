using Godot;

namespace Game.Utilities.Loaded;

public static class LoadedScenes
{
    public static readonly PackedScene MainMenu = GD.Load<PackedScene>("uid://52bgij52xouq");
    public static readonly PackedScene Settings = GD.Load<PackedScene>("uid://bw4k558m3yh1h");
    public static readonly PackedScene GameoverScreen = GD.Load<PackedScene>("uid://d3seefhkbo7ky");
}
