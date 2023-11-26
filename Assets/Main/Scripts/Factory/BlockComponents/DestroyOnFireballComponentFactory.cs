using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class DestroyOnFireballComponentFactory : IBlockComponentFactory
    {
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            DestroyOnFireball destroyOnFireball = block.AddComponent<DestroyOnFireball>();
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out DestroyOnFireball destroyOnFireball))
            {
                Object.Destroy(destroyOnFireball);
            }
        }
    }
}