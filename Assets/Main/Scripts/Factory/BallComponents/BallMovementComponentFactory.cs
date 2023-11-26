using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BallComponents
{
    public class BallMovementComponentFactory : IBallComponentFactory
    {
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Ball ball)
            {
                return;
            }
            BallMovement ballMovement = ball.AddComponent<BallMovement>();
            ballMovement.Construct(serviceContainer.Get<ITimeProvider>(), serviceContainer.Get<IBallSpeedSystem>(), ball.Rigidbody);
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out BallMovement ballMovement))
            {
                Object.Destroy(ballMovement);
            }
        }
    }
}