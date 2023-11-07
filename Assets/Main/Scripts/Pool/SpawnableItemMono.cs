using UnityEngine;

namespace Main.Scripts.Pool
{
    public class SpawnableItemMono : MonoBehaviour, ISpawnableItem
    {
        private IObjectPool _objectPool;
        
        public void OnCreated(IObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public void OnSpawned()
        {
        }

        public void OnDespawned()
        {
        }

        public void OnRemoved()
        {
            if (_objectPool is null)
            {
                OnRemoved();
                return;
            }

            _objectPool.Despawn(this);
        }
    }
}