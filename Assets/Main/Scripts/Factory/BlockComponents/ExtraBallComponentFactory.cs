using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class ExtraBallComponentFactory : IBlockComponentFactory
    {
        public string ExtraBallEffectKey;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            ExtraBall extraBall = block.AddComponent<ExtraBall>();
            extraBall.Construct(block, serviceContainer.Get<IExtraBallSystem>(), serviceContainer.Get<IEffectFactory>(), ExtraBallEffectKey);
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out ExtraBall extraBall))
            {
                Object.Destroy(extraBall);
            }
        }
    }
}