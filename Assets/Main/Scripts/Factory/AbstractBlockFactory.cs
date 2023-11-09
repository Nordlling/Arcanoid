using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public interface IBlockFactory
    {
        int ClassId { get; }
        Block Spawn(BlockSpawnContext spawnContext);
        void Despawn(Block gameObject);
    }
    
    public abstract class AbstractBlockFactory : MonoBehaviour, IBlockFactory
    {
        
        public abstract Block Spawn(BlockSpawnContext spawnContext);
        public abstract void Despawn(Block gameObject);
        public int ClassId { get; private set; }
    }
}