using System.Collections.Generic;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class AudioManager : Node
{
    [Export]
    public AudioStreamPlayer MusicPlayer { get; private set; }

    [Export]
    public Node SfxPool { get; private set; }

    [Export]
    public int SfxPoolMaxSize { get; private set; } = 32;
    public List<AudioStreamPlayer> SfxPlayerList { get; private set; } = [];
    public static AudioManager Instance { get; private set; }
    private const string MUSIC_BUS_NAME = "Music";
    private const string SFX_BUS_NAME = "Sfx";

    public override void _Ready()
    {
        Instance = this;

        MusicPlayer.Bus = MUSIC_BUS_NAME;
        CreateSfxPool();
    }

    public void CreateSfxPool()
    {
        for (int i = 0; i < SfxPoolMaxSize; i++)
        {
            var player = new AudioStreamPlayer { Bus = SFX_BUS_NAME };

            SfxPlayerList.Add(player);
            SfxPool.AddChild(player);
        }
    }

    public void PlaySfx(AudioStream sfx)
    {
        foreach (AudioStreamPlayer player in SfxPlayerList)
        {
            if (player.Playing)
            {
                continue;
            }

            player.Stream = sfx;
            player.Play();
            break;
        }
    }

    public void SetVolume(string busName, float value)
    {
        var index = AudioServer.GetBusIndex(busName);

        AudioServer.SetBusVolumeDb(index, Mathf.LinearToDb(value));
    }
}
