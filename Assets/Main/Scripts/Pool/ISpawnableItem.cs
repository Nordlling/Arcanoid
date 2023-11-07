namespace Main.Scripts.Pool
{
    public interface ISpawnableItem
    {
        void OnCreated(IObjectPool objectPool);
        void OnSpawned();
        void OnDespawned();
        void OnRemoved();
    }
}