using Game.Utilities.World.Maps;

namespace Game.Common.Cutscenes;

public record CutsceneContext
{
    public Level Level { get; init; }
}
