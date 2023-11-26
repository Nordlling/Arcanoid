using Main.Scripts.Configs;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public class BallFactory : IBallFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly BallsConfig _ballsConfig;
        private readonly PoolProvider _poolProvider;

        public BallFactory(ServiceContainer serviceContainer, BallsConfig ballsConfig, PoolProvider poolProvider)
        {
            _serviceContainer = serviceContainer;
            _ballsConfig = ballsConfig;
            _poolProvider = poolProvider;
        }

        public Ball Spawn(SpawnContext spawnContext)
        {
            if (!_ballsConfig.BallInfos.ContainsKey(spawnContext.ID))
            {
                return null;
            }
            
            BallInfo ballInfo = _ballsConfig.BallInfos[spawnContext.ID];
            Ball ball = (Ball)_poolProvider.PoolItemView.Spawn();
            
            ball.Construct(spawnContext.ID, this);
            
            if (spawnContext.Parent is not null)
            {
                ball.transform.parent = spawnContext.Parent;
            }
            
            if (spawnContext.Position != Vector2.zero)
            {
                ball.transform.position = spawnContext.Position;
            }
           
            ball.SpriteRenderer.sprite = ballInfo.BasicInfo.Visual;
            ball.CollisionDetector.Construct(_serviceContainer.Get<IBallCollisionService>());
            ball.Collider.radius = ball.SpriteRenderer.bounds.extents.x;

            IBallComponentFactory[] componentFactories = ballInfo.ComponentFactories;
            
            foreach (IBallComponentFactory componentFactory in componentFactories)
            {
                componentFactory.AddComponent(_serviceContainer, ball, spawnContext);
            }
            
            return ball;
        }

        public void Despawn(Ball ball)
        {
            IBallComponentFactory[] componentFactories = _ballsConfig.BallInfos[ball.ID].ComponentFactories;
            
            foreach (IBallComponentFactory componentFactory in componentFactories)
            {
                componentFactory.RemoveComponent(ball);
            }
            
            _poolProvider.PoolItemView.Despawn(ball);
        }
    }
}