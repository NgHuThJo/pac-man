using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Game.Utilities.World.Maps;
using Godot;

namespace Game.World;

public partial class Main : Node
{
    public override void _Ready()
    {
        SceneManager.Instance.ChangeScene<MainMenu>(LoadedScenes.MainMenu);
    }
}
