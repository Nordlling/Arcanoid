using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;

namespace Main.Scripts.Factory.Components
{
    public class ExplosionComponentFactory : IComponentFactory
    {
        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            Explosion explosion = block.AddComponent<Explosion>();
        }
    }
}