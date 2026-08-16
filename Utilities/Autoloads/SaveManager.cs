using System.Text.Json;
using Game.Common.Persistence;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class SaveManager : Node
{
    public static SaveManager Instance { get; private set; }
    public GameSaveState GameSaveState { get; private set; } = new();
    public const string SAVEDATA_FOLDERPATH = "user://saves";
    public const string SAVEDATA_FILEPATH = $"{SAVEDATA_FOLDERPATH}/save.json";

    public override void _EnterTree()
    {
        Load();
    }

    public override void _Ready()
    {
        Instance = this;
    }

    public void Save()
    {
        if (!DirAccess.DirExistsAbsolute(SAVEDATA_FOLDERPATH))
        {
            DirAccess.MakeDirRecursiveAbsolute(SAVEDATA_FOLDERPATH);
        }

        foreach (var node in GetTree().GetNodesInGroup("Saveable"))
        {
            if (node is ISaveable instance)
            {
                instance.Save();
            }
        }

        GD.Print("Game save data, ", GameSaveState);

        var json = JsonSerializer.Serialize(
            GameSaveState,
            new JsonSerializerOptions { WriteIndented = true }
        );
        var tempPath = $"{SAVEDATA_FOLDERPATH}/temp.json";

        using (var file = FileAccess.Open(tempPath, FileAccess.ModeFlags.Write))
        {
            if (file is null)
            {
                GD.PushError("Failed to open game save file for writing");
                return;
            }

            if (!file.StoreString(json))
            {
                GD.PushError("Failed to store JSON string of game save data");
                return;
            }
        }

        var error = DirAccess.RenameAbsolute(tempPath, SAVEDATA_FILEPATH);

        if (error != Error.Ok)
        {
            GD.PushError("Failed to rename temp game save file to original game save file name");
            return;
        }
    }

    public void Load()
    {
        using var file = FileAccess.Open(SAVEDATA_FILEPATH, FileAccess.ModeFlags.Read);
        var json = file.GetAsText();

        try
        {
            GameSaveState = JsonSerializer.Deserialize<GameSaveState>(
                json,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }
        catch (JsonException ex)
        {
            GD.PushError($"Failed to to load game save data: {ex.Message}");
        }
    }
}
