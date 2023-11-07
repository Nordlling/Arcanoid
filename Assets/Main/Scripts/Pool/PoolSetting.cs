using System;
using UnityEngine;

namespace Main.Scripts.Pool
{
    [Serializable]
    public class PoolSetting<T> where T : SpawnableItemMono
    {
        public T SpawnableItemMono;
        public Transform Container;
        public int StartSize;
    }
}