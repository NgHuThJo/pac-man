namespace Game.Common.GameEvents.Settings;

public record VolumeValueChanged : IGameEvent
{
    public string BusName { get; set; }
}
