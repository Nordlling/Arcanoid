using System;
using System.Collections.Generic;

namespace Main.Scripts.Pool
{
    public class ObjectPool<T> : IObjectPool<T>, IObjectPool where T : class
    {
        private readonly Func<T> _spawner;
        private readonly Stack<T> _items = new();
        private int _size;
        
        public int Count => _items.Count;

        protected ObjectPool(Func<T> spawner, int initSize)
        {
            _spawner = spawner;
            Resize(initSize);
        }
        
        public T Spawn()
        {
            T item = _items.Count > 0 ? _items.Pop() : CreatePullItem();
            OnItemSpawned(item);
            return item;
        }

        public void Resize(int count)
        {
            int countDifference = count - _size;
            if (countDifference > 0)
            {
                AddElements(countDifference);
            }
        }

        public void Despawn(object item)
        {
            if (item is T typeItem)
            {
                _items.Push(typeItem);
                OnDespawnItem(typeItem);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public T Create()
        {
            return Spawn();
        }

        private void AddElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _items.Push(CreatePullItem());
            }
        }

        private T CreatePullItem()
        {
            T item = _spawner.Invoke();
            OnItemCreated(item);
            return item;
        }

        protected virtual void OnItemSpawned(T item)
        {
        }

        protected virtual void OnDespawnItem(T item)
        {
        }

        protected virtual void OnItemCreated(T item)
        {
        }
    }
}