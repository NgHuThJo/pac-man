using Godot;

namespace Game.Entities.Items;

[GlobalClass]
public partial class ItemData : Resource
{
    [Export]
    public int Points { get; private set; }
}
