using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.Components
{
    public class DestroyOnExplodeComponentFactory : IComponentFactory
    {
        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            DestroyOnExplode destroyOnExplode = block.AddComponent<DestroyOnExplode>();
        }
        
        public void RemoveComponent(Block block)
        {
            if (block.TryGetComponent(out DestroyOnExplode destroyer))
            {
                Object.Destroy(destroyer);
            }
        }
    }
}