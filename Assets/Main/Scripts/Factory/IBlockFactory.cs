using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Factory
{
    public interface IBlockFactory
    {
        Block Spawn(BlockSpawnContext spawnContext);
        void Despawn(Block gameObject);
    }
}