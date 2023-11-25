using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Pool;

namespace Main.Scripts.Factory.Components
{
    public interface IComponentFactory
    {
        void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono;
        void RemoveComponent<T>(T unit) where T : SpawnableItemMono;
    }
    
    public interface IBlockComponentFactory : IComponentFactory
    {
    }
    
    public interface IBoostComponentFactory : IComponentFactory
    {
    }
}