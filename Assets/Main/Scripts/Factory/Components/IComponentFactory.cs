using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Factory.Components
{
    public interface IComponentFactory
    {
        void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext);
        void RemoveComponent(Block block);
    }
}