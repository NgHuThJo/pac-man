using System;
using Game.Utilities.Autoloads;
using Godot;

namespace Game.UI.Settings;

public partial class MusicSlider : HBoxContainer
{
    public event Action<string, double> ValueChanged;

    [Export]
    public Label Label { get; set; }

    [Export]
    public string LabelText { get; set; }

    [Export]
    public string BusName { get; set; }

    [Export]
    public string ConfigStringKey { get; set; }

    [Export]
    public HSlider Slider { get; set; }

    [Export]
    public float MinValue { get; set; }

    [Export]
    public float MaxValue { get; set; }

    [Export]
    public float Step { get; set; }

    [Export]
    public float StartValue { get; set; }

    public override void _Ready()
    {
        Label.Text = LabelText;

        Slider.MinValue = MinValue;
        Slider.MaxValue = MaxValue;
        Slider.Step = Step;
        Slider.Value = StartValue;
        Slider.ValueChanged += OnValueChanged;
    }

    public void Initialize(float value)
    {
        Slider.Value = value;
    }

    private void OnValueChanged(double value)
    {
        AudioManager.Instance.SetVolume(BusName, (float)value);
        ValueChanged?.Invoke(ConfigStringKey, value);
    }
}
