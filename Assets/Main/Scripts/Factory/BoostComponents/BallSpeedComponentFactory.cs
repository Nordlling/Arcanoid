using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls.BallBoosts;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class BallSpeedComponentFactory : IBoostComponentFactory
    {   
        public SpeedBallConfig SpeedBallConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            BallBoostSpeed ballBoostSpeed = boost.AddComponent<BallBoostSpeed>();
            ballBoostSpeed.Construct(boost, SpeedBallConfig, serviceContainer.Get<IBallSpeedSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out BallBoostSpeed ballBoostSpeed))
            {
                Object.Destroy(ballBoostSpeed);
            }
        }
    }
}