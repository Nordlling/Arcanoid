using Main.Scripts.Factory;

namespace Main.Scripts.Pool
{
    public interface IObjectPool
    {
        int Count { get; }
        void Resize(int count);
        void Despawn(object item);
        void Clear();
    }

    public interface IObjectPool<out T> : IFactory<T>
    {
        T Spawn();
    }
}