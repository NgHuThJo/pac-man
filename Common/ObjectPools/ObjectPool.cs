using System.Collections.Generic;
using Godot;

namespace Game.Common.ObjectPools;

public partial class ObjectPool<T> : Node
    where T : Node, IPoolable<T>
{
    [Export]
    public PackedScene EntityScene { get; private set; }

    [Export(PropertyHint.Range, "1,10,1")]
    public int InitialSize { get; private set; } = 10;

    [Export]
    public int QueueMaxSize { get; private set; } = 100;
    private Queue<T> AvailableEntities { get; init; } = [];

    public override void _Ready()
    {
        for (int i = 0; i < InitialSize; i++)
        {
            var instance = EntityScene.Instantiate<T>();

            instance.ObjectPool = this;
            instance.OnDespawn();
            CallDeferred(Node.MethodName.AddChild, instance);
            AvailableEntities.Enqueue(instance);
        }
    }

    public T GetItem()
    {
        T entity;

        if (AvailableEntities.Count > 0)
        {
            entity = AvailableEntities.Dequeue();
        }
        else
        {
            entity = EntityScene.Instantiate<T>();
            entity.ObjectPool = this;
            AddChild(entity);
        }

        entity.OnSpawn();

        return entity;
    }

    public void Release(T entity)
    {
        entity.OnDespawn();

        if (AvailableEntities.Count < QueueMaxSize)
        {
            AvailableEntities.Enqueue(entity);
        }
        else
        {
            SetDeferred(Node.MethodName.QueueFree, entity);
        }
    }
}
