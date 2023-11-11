using UnityEngine;

namespace Main.Scripts.Pool
{
    public class PoolProvider : MonoBehaviour
    {
        [SerializeField] private PoolSetting<SpawnableItemMono> _poolSetting;

        public ObjectPoolMono<SpawnableItemMono> PoolItemView { get; private set; }
        public PoolProviderMono<SpawnableItemMono> PoolProviderMono { get; private set; }

        public void Init()
        {
            PoolProviderMono = new PoolProviderMono<SpawnableItemMono>(_poolSetting);
            PoolItemView = PoolProviderMono.ObjectPool;
        }
    }
}