using Game.UI.Settings;
using Game.Utilities.Autoloads;
using Godot;

namespace Utils;

public partial class SoundTab : MarginContainer
{
    [Export]
    public MusicSlider Master { get; set; }

    [Export]
    public MusicSlider Music { get; set; }

    [Export]
    public MusicSlider Sfx { get; set; }

    public override void _Ready()
    {
        Master.Initialize(ConfigManager.Instance.GameSettings.MasterVolume);
        Music.Initialize(ConfigManager.Instance.GameSettings.MusicVolume);
        Sfx.Initialize(ConfigManager.Instance.GameSettings.SfxVolume);

        Master.ValueChanged += OnValueChanged;
        Music.ValueChanged += OnValueChanged;
        Sfx.ValueChanged += OnValueChanged;
    }

    public void OnValueChanged(string configStringKey, double value)
    {
        GD.Print("OnValueChanged: ", configStringKey, " ", value);

        var convertedValue = (float)value;

        switch (configStringKey)
        {
            case "Master":
            {
                ConfigManager.Instance.GameSettings.MasterVolume = convertedValue;
                break;
            }
            case "Music":
            {
                ConfigManager.Instance.GameSettings.MusicVolume = convertedValue;
                break;
            }
            case "Sfx":
            {
                ConfigManager.Instance.GameSettings.SfxVolume = convertedValue;
                break;
            }
        }
    }
}
