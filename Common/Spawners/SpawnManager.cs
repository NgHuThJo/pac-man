using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Game.Common.Spawners;

public partial class SpawnManager : Node
{
    public List<SpawnPoint> SpawnPoints { get; private set; }

    public override void _Ready()
    {
        var spawnPoints = GetTree().GetNodesInGroup("SpawnPoints").Cast<SpawnPoint>().ToList();
        SpawnPoints = spawnPoints;
    }

    public override void _Process(double delta) { }
}
