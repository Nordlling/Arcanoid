using UnityEngine;

namespace Main.Scripts.Factory
{
    public interface IFactoryUnit
    {
        GameObject Spawn(UnitSpawnContext spawnContext);
        void Despawn(GameObject gameObject);
    }
    
    public abstract class AbstractFactoryUnit : IFactoryUnit
    {
        public abstract GameObject Spawn(UnitSpawnContext spawnContext);
        public abstract void Despawn(GameObject gameObject);
    }
}