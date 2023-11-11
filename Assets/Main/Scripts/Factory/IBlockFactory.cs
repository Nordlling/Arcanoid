using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Factory
{
    public interface IBlockFactory
    {
        Block Spawn(SpawnContext spawnContext);
        void Despawn(Block gameObject);
    }
}