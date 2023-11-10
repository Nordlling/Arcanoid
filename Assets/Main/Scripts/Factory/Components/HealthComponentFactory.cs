using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;

namespace Main.Scripts.Factory
{
    public class HealthComponentFactory : IComponentFactory
    {
        public int HealthCount;

        public void AddComponent(ServiceContainer serviceContainer, Block block, BlockSpawnContext spawnContext)
        {
            Health health = block.AddComponent<Health>();
            health.Construct(HealthCount);
            block.Subscribe(health);
        }
    }
}