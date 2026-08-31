using Godot;

namespace Game.World.Maps;

public enum CellType
{
    Wall,
    Floor,
}

public partial class Map : TileMapLayer
{
    public const int GRID_WIDTH = 22;
    public const int GRID_HEIGHT = 28;
    public const int CELL_SIZE = 16;

    public static bool IsInsideGrid(Vector2I position)
    {
        var (x, y) = position;

        return 0 <= x && x < GRID_WIDTH && 0 <= y && y < GRID_HEIGHT;
    }

    public static Vector2 SnapToGrid(Vector2 direction)
    {
        var (x, y) = direction;

        var snappedX = Mathf.FloorToInt(x / CELL_SIZE) * CELL_SIZE + CELL_SIZE / 2;
        var snappedY = Mathf.FloorToInt(y / CELL_SIZE) * CELL_SIZE + CELL_SIZE / 2;

        return new(snappedX, snappedY);
    }

    public static bool IsAlignedWithGrid(
        Vector2 currentPosition,
        Vector2 direction,
        float tolerance = 2f
    )
    {
        var snapped = SnapToGrid(currentPosition);

        if (direction.X != 0)
        {
            return Mathf.Abs(currentPosition.X - snapped.X) < tolerance;
        }
        if (direction.Y != 0)
        {
            return Mathf.Abs(currentPosition.Y - snapped.Y) < tolerance;
        }

        return false;
    }

    public static Vector2 SnapForTurn(Vector2 currentPosition, Vector2 direction)
    {
        var snapped = SnapToGrid(currentPosition);

        if (direction.X != 0)
        {
            return new(snapped.X, currentPosition.X);
        }

        if (direction.Y != 0)
        {
            return new(snapped.Y, currentPosition.Y);
        }

        return Vector2.Zero;
    }

    public void Turn(Vector2 currentPosition)
    {
        var snapped = SnapToGrid(currentPosition);
    }
}
