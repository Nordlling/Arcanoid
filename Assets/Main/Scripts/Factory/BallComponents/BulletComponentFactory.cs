using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BallComponents
{
    public class BulletComponentFactory : IBallComponentFactory
    {
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Ball ball)
            {
                return;
            }
            Bullet bullet = ball.AddComponent<Bullet>();
            ball.Collider.isTrigger = true;
            bullet.Construct(ball, serviceContainer.Get<IBallContainer>(), serviceContainer.Get<ITimeProvider>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit is not Ball ball)
            {
                return;
            }

            ball.Collider.isTrigger = false;
            if (unit.TryGetComponent(out Bullet bullet))
            {
                Object.Destroy(bullet);
            }
        }
    }
}