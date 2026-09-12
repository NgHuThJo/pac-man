using System;
using System.Collections.Generic;
using Godot;

namespace Game.Common.Spatial;

public class QuadTree<T>(int capacity, int maxDepth, Rect2 bounds, Func<T, Vector2> getPosition)
{
    private QuadTree(
        int capacity,
        int maxDepth,
        Rect2 bounds,
        Func<T, Vector2> getPosition,
        int depth
    )
        : this(capacity, maxDepth, bounds, getPosition)
    {
        Depth = depth;
    }

    private List<T> Positions { get; } = [];
    public int Capacity { get; } = capacity;
    public int MaxDepth { get; } = maxDepth;
    public Rect2 Bounds { get; } = bounds;
    public Func<T, Vector2> GetPosition { get; } = getPosition;

    public List<QuadTree<T>> Children { get; } = [];
    public int Depth { get; private set; } = 0;

    public bool Insert(T item)
    {
        var itemPosition = GetPosition(item);

        if (!Bounds.HasPoint(itemPosition))
        {
            return false;
        }

        if (Children.Count > 0)
        {
            foreach (var child in Children)
            {
                if (child.Bounds.HasPoint(itemPosition) && child.Insert(item))
                {
                    return true;
                }
            }

            return false;
        }

        Positions.Add(item);

        if (Positions.Count >= Capacity && Depth < MaxDepth)
        {
            Split();
        }

        return true;
    }

    private void Split()
    {
        var boundsPosition = Bounds.Position;
        var halfWidth = Bounds.Size.X / 2;
        var halfHeight = Bounds.Size.Y / 2;
        var nextDepth = Depth + 1;

        Children.AddRange(
            new QuadTree<T>(
                Capacity,
                MaxDepth,
                new Rect2(boundsPosition, halfWidth, halfHeight),
                GetPosition,
                nextDepth
            ),
            new QuadTree<T>(
                Capacity,
                MaxDepth,
                new Rect2(
                    new Vector2(boundsPosition.X + halfWidth, boundsPosition.Y),
                    halfWidth,
                    halfHeight
                ),
                GetPosition,
                nextDepth
            ),
            new QuadTree<T>(
                Capacity,
                MaxDepth,
                new Rect2(
                    new Vector2(boundsPosition.X, boundsPosition.Y + halfHeight),
                    halfWidth,
                    halfHeight
                ),
                GetPosition,
                nextDepth
            ),
            new QuadTree<T>(
                Capacity,
                MaxDepth,
                new Rect2(
                    new Vector2(boundsPosition.X + halfWidth, boundsPosition.Y + halfHeight),
                    halfWidth,
                    halfHeight
                ),
                GetPosition,
                nextDepth
            )
        );

        foreach (var item in Positions)
        {
            foreach (var child in Children)
            {
                if (child.Bounds.HasPoint(GetPosition(item)) && child.Insert(item))
                {
                    break;
                }
            }
        }

        Positions.Clear();
    }

    public bool Delete(T item)
    {
        var position = GetPosition(item);

        if (!Bounds.HasPoint(position))
        {
            return false;
        }

        if (Children.Count == 0)
        {
            return Positions.Remove(item);
        }

        foreach (var child in Children)
        {
            if (child.Bounds.HasPoint(position))
            {
                return child.Delete(item);
            }
        }

        return false;
    }

    public List<T> Query(Rect2 region)
    {
        if (!Bounds.Intersects(region))
        {
            return [];
        }

        List<T> result = [];

        if (Children.Count > 0)
        {
            foreach (var child in Children)
            {
                result.AddRange(child.Query(region));
            }
        }

        foreach (var item in Positions)
        {
            if (region.HasPoint(GetPosition(item)))
            {
                result.Add(item);
            }
        }

        return result;
    }
}
