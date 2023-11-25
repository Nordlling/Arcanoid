using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class DestroyOnExplodeComponentFactory : IBlockComponentFactory
    {
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            DestroyOnExplode destroyOnExplode = block.AddComponent<DestroyOnExplode>();
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out DestroyOnExplode destroyer))
            {
                Object.Destroy(destroyer);
            }
        }
    }
}