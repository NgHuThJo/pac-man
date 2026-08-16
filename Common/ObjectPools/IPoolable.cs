using Godot;

namespace Game.Common.ObjectPools;

public interface IPoolable<T>
    where T : Node, IPoolable<T>
{
    ObjectPool<T> ObjectPool { get; set; }

    void OnSpawn();

    void OnDespawn();
}
