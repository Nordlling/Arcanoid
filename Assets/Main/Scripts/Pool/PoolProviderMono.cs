using System;
using Object = UnityEngine.Object;

namespace Main.Scripts.Pool
{
    public class PoolProviderMono<T> where T : SpawnableItemMono
    {
        public ObjectPoolMono<T> ObjectPool;

        public PoolProviderMono(PoolSetting<T> poolSetting)
        {
            ObjectPool = CreatePool(poolSetting);
        }

        private ObjectPoolMono<T> CreatePool(PoolSetting<T> poolSetting)
        {
            var spawner = new Func<T>(() => Object.Instantiate(poolSetting.SpawnableItemMono, poolSetting.Container));
            return new ObjectPoolMono<T>(spawner, poolSetting.StartSize);
        }
    }
}