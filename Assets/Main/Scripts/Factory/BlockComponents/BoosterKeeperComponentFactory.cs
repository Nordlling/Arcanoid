using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class BoosterKeeperComponentFactory : IBlockComponentFactory
    {
        public string BoostID;
        public string EffectKey;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            BoostKeeper boosterKeeper = block.AddComponent<BoostKeeper>();
            boosterKeeper.Construct(
                block, 
                serviceContainer.Get<IBoostContainer>(), 
                serviceContainer.Get<IEffectFactory>(),
                BoostID,
                EffectKey);
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out BoostKeeper BoosterKeeper))
            {
                Object.Destroy(BoosterKeeper);
            }
        }
    }
}