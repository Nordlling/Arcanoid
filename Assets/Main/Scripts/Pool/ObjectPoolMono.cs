using System;
using UnityEngine;

namespace Main.Scripts.Pool
{
    public class ObjectPoolMono<T> : ObjectPool<T> where T : Component, ISpawnableItem
    {
        public ObjectPoolMono(Func<T> spawner, int initSize) : base(spawner, initSize)
        {
        }

        protected override void OnItemCreated(T item)
        {
            base.OnItemCreated(item);
            DisableItem(item);
            item.OnCreated(this);
        }

        protected override void OnItemSpawned(T item)
        {
            base.OnItemSpawned(item);
            item.gameObject.SetActive(true);
            item.OnSpawned();
        }

        protected override void OnDespawnItem(T item)
        {
            base.OnDespawnItem(item);
            DisableItem(item);
            item.OnDespawned();
        }

        private void DisableItem(T item)
        {
            item.gameObject.SetActive(false);
        }
    }
}