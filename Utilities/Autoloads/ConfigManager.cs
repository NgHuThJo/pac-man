using System.Text.Json;
using Game.UI.Settings;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class ConfigManager : Node
{
    public static ConfigManager Instance { get; private set; }
    public const string CONFIG_FOLDERPATH = "user://config";
    public const string CONFIG_FILEPATH = $"{CONFIG_FOLDERPATH}/config.json";
    public const int CONFIG_VERSION = 1;
    public GameSettings GameSettings { get; private set; } =
        new GameSettings { Version = CONFIG_VERSION };

    public override void _Ready()
    {
        Instance = this;
        LoadConfig();
    }

    public void SaveConfig()
    {
        DirAccess.MakeDirRecursiveAbsolute(CONFIG_FOLDERPATH);

        var json = JsonSerializer.Serialize(
            GameSettings,
            new JsonSerializerOptions { WriteIndented = true }
        );
        const string tempPath = $"{CONFIG_FOLDERPATH}/temp.json";

        using (var file = FileAccess.Open(tempPath, FileAccess.ModeFlags.Write))
        {
            if (file is null)
            {
                GD.PushError("Failed to open config file for writing");
                return;
            }

            if (!file.StoreString(json))
            {
                GD.PushError("Failed to store JSON string of config data");
                return;
            }
        }

        var error = DirAccess.RenameAbsolute(tempPath, CONFIG_FILEPATH);

        if (error != Error.Ok)
        {
            GD.PushError("Failed to rename temp config file to original config file name");
            return;
        }
    }

    public void LoadConfig()
    {
        using var file = FileAccess.Open(CONFIG_FILEPATH, FileAccess.ModeFlags.Read);

        if (file is null)
        {
            GD.PushError("Failed to open config file for reading");
            return;
        }

        try
        {
            var json = file.GetAsText();

            GameSettings = JsonSerializer.Deserialize<GameSettings>(
                json,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }
        catch (JsonException ex)
        {
            GD.PushError($"Failed to to load config data: {ex.Message}");
        }
    }
}
