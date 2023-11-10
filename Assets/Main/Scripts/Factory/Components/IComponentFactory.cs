using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Factory
{
    public interface IComponentFactory
    {
        void AddComponent(ServiceContainer serviceContainer, Block block, BlockSpawnContext spawnContext);
    }
}