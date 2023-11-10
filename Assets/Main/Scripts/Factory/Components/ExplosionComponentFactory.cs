using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;

namespace Main.Scripts.Factory
{
    public class ExplosionComponentFactory : IComponentFactory
    {
        public void AddComponent(ServiceContainer serviceContainer, Block block, BlockSpawnContext spawnContext)
        {
            Explosion explosion = block.AddComponent<Explosion>();
        }
    }
}