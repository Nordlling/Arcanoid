using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Pool;

namespace Main.Scripts.Factory
{
    public class BallFactory : IBallFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly PoolProvider _poolProvider;

        public BallFactory(ServiceContainer serviceContainer, PoolProvider poolProvider)
        {
            _serviceContainer = serviceContainer;
            _poolProvider = poolProvider;
        }

        public Ball Spawn(SpawnContext spawnContext)
        {
            Ball ball = (Ball)_poolProvider.PoolItemView.Spawn();
            
            ball.Construct(spawnContext.ID, this);
            ball.CollisionDetector.Construct(_serviceContainer.Get<IBallCollisionService>());
            ball.Collider.radius = ball.SpriteRenderer.bounds.extents.x;
            ball.BallMovement.Construct(_serviceContainer.Get<ITimeProvider>(), _serviceContainer.Get<IBallSpeedSystem>());
            ball.transform.parent = spawnContext.Parent;
            
            return ball;
        }

        public void Despawn(Ball ball)
        {
            _poolProvider.PoolItemView.Despawn(ball);
        }
    }
}